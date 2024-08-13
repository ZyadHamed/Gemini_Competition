# Libraries
import os
import os.path
import base64
from bs4 import BeautifulSoup
from google.auth.transport.requests import Request
from google.oauth2.credentials import Credentials
from google_auth_oauthlib.flow import InstalledAppFlow
from googleapiclient.discovery import build

# If modifying these SCOPES, delete the file token.json.
SCOPES = ['https://www.googleapis.com/auth/gmail.readonly']
absolutePath = os.path.abspath(".")
# Used to parse the email and download the body text by extracting the data from the payload
def parse_msg(msg):
    payload = msg['payload']
    if data := payload['body'].get('data'):
        return parse_data(data)
    combinedString = ""
    for part in payload['parts']:
        if "data" not in part['body']:
            print("Malfunctioned part:", part['body'].keys())
        else:
            combinedString += parse_data(part['body']['data'])
    return combinedString
# Parses the actual text of the email
def parse_data(data):
    return base64.urlsafe_b64decode(data.encode('ASCII')).decode('utf-8')

# Function to extract the subject
def get_subject(headers):
    for header in headers:
        if header['name'] == "Subject":
            return header['value']
    return "No Subject Found"

def main():
    creds = None
    # The file token.json stores the user's access and refresh tokens, and is
    # created automatically when the authorization flow completes for the first
    # time.
    if os.path.exists(os.path.join(absolutePath, 'token.json')):
        creds = Credentials.from_authorized_user_file(os.path.join(absolutePath, 'token.json'), SCOPES)
    # If there are no (valid) credentials available, let the user log in.
    if not creds or not creds.valid:
        if creds and creds.expired and creds.refresh_token:
            creds.refresh(Request())
        else:
            flow = InstalledAppFlow.from_client_secrets_file(
                os.path.join(absolutePath, 'credentials.json'), SCOPES)
            creds = flow.run_local_server(port=0)
        # Save the credentials for the next run
        with open(os.path.join(absolutePath, 'token.json'), 'w') as token:
            token.write(creds.to_json())

    service = build('gmail', 'v1', credentials=creds)

    # Call the Gmail API/Specify the number of emails to download
    query = 'label:IMPORTANT'
    results = service.users().messages().list(userId='me', maxResults=10, q=query).execute()
    messages = results.get('messages', [])

    if not messages:
        print('No messages found.')
    else:
        print('Messages:')
        for message in messages:
            msg = service.users().messages().get(userId='me', id=message['id'], format='full').execute()
            
            
            subject = get_subject(msg['payload'].get('headers')).encode('utf-8')
            print(f"Subject: {subject}")
           
            # Print snippet (Most liekly first 50-100 chars)
            # We need to use UTF-8 encoding or else we get an error that says 
            # "UnicodeEncodeError: 'charmap' codec can't encode characters in position" for some emails
            my_snippet = msg['snippet'].encode('utf-8')
            print(f"Snippet: {my_snippet}")

            # Gets the body of the email and saves it
            body = parse_msg(msg).encode('utf-8')
            string_body = body.decode('utf-8')
            soup = BeautifulSoup(string_body, 'html.parser')
            text_content = soup.get_text()
            text_content = text_content.strip()
            text_content = ' '.join(text_content.split())
            body = text_content.encode('utf-8')
            with open(f"{message['id']}.txt", 'wb') as msg_file:
                msg_file.write(body)

if __name__ == '__main__':
    main()