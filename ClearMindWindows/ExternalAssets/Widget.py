import tkinter as tk
import threading as th
import liveTranscription as sd


root = tk.Tk()
root.title("Recorder")

thread_loop = None
running = False
def start():
    global running, thread_loop
    while not running:
        running = True
        thread_loop = th.Thread(target=sd.start_recording)
        thread_loop.start()

def stop():
    global running
    running = False
    sd.stop_recording()


# Start Button
#start_btn = tk.Button(root, text="Start Recording", command=start)
#start_btn.pack()

# Stop Button
#stop_btn = tk.Button(root, text="Stop Recording", command=stop)
#stop_btn.pack()



#root.mainloop()
start()