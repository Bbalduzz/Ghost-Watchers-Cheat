
import os
import subprocess
import tkinter
import customtkinter

class Success(customtkinter.CTk):
    def __init__(self):
        super().__init__()

        self.title("Injection successfull")
        self.minsize(200, 120)
        self.button = customtkinter.CTkButton(master=self, text="Success!", command=self.button_callback)
        self.button.pack(padx=20, pady=20)

    def button_callback(self):
        self.destroy()
        app.destroy()

class Faililure(customtkinter.CTk):
    def __init__(self, error):
        super().__init__()

        self.title("Injection failed!")
        self.minsize(200, 120)
        self.textbox = customtkinter.CTkTextbox(master=self,)
        self.textbox.grid(row=0, column=0)
        self.textbox.insert("0.0", error) 
        self.text = self.textbox.get("0.0", "end") 
        self.textbox.configure(state="disabled")

        self.button = customtkinter.CTkButton(master=self, text="ok", command=self.button_callback)
        self.button.grid(row=1, column=0)
    def button_callback(self):
        self.destroy()

customtkinter.set_appearance_mode("dark")
customtkinter.set_default_color_theme("blue")
customtkinter.set_widget_scaling(1.5)

app = customtkinter.CTk()
app.geometry("400x240")
app.resizable(False, False)
app.title("BbalduzzGW Injector")

def injection_function():
    # print(f"{os.path.abspath(os.path.join(os.getcwd(), os.pardir))}\\bin\\Debug\\net6.0\\Ghost-Watchers-Internal.dll") #formula 4 debug, not in the release
    command = f'MonoJabber.exe "Ghost Watchers.exe" "{os.path.abspath(os.getcwd())}\\Ghost-Watchers-Internal.dll" "Ghost_Watchers_Internal" "Loader" "init"'
    output = subprocess.check_output(command, shell=True)
    print(output)
    if 'Injection and RuntimeInvoke were successful' in output.decode():
        Success().mainloop()
    else:
        subprocess.run('\n', shell=True)
        Faililure(output.decode()).mainloop()

def unload_function():
    command = f'MonoJabber.exe "Ghost Watchers.exe" "{os.path.abspath(os.getcwd())}\\Ghost-Watchers-Internal.dll" "Ghost_Watchers_Internal" "Ghost_Watchers_Internal" "Loader" "unload"'
    output = subprocess.check_output(command, shell=True)
    print(output)
    if 'Injection and RuntimeInvoke were successful' in output.decode():
        Success().mainloop()
    else:
        subprocess.run('\n', shell=True)
        Faililure(output.decode()).mainloop()


label = customtkinter.CTkLabel(master=app, text="Ghost Watchers")
label.place(relx=0.5, rely=0.15, anchor=tkinter.CENTER)
label2 = customtkinter.CTkLabel(master=app, text="Injector", font=customtkinter.CTkFont(size=18, weight="bold",underline=True))
label2.place(relx=0.5, rely=0.3, anchor=tkinter.CENTER)

loadBTN = customtkinter.CTkButton(master=app, text="Inject", command=injection_function)
loadBTN.place(relx=0.5, rely=0.6, anchor=tkinter.CENTER)

unloadBTN = customtkinter.CTkButton(master=app, text="Unload", command=unload_function)
unloadBTN.place(relx=0.5, rely=0.8, anchor=tkinter.CENTER)

app.mainloop()
