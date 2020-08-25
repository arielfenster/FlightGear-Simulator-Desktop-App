# FlightGear Simulator Desktop Application
A desktop application for the FlightGear flight simulator, used to fly the aircraft using either a joystick or a stream of commands. <br/>
Implmented using WPF technology and MVVM architecture.

### Installing 
* Download and install the simulator on your computer- https://www.flightgear.org/download/
* Add the  generic_small.xml file to the /data/Protocol directory where you installed the simulator
* Config the following settings in the 'Settings' tab in the simulator:
```
--telnet=socket,in,10,127.0.0.1,5402,tcp
--generic=socket,out,10,127.0.0.1,5400,tcp,generic_small
```
![simulator-settings-config](https://user-images.githubusercontent.com/45856261/58368127-4a489680-7ef1-11e9-81ca-b17badca7f8e.PNG)

This will open two communication sockets - 'in' where you send commands to the simulator, and 'out' where you receive data from it.

### About the app
On startup, the main window will be presented:

![main window](https://user-images.githubusercontent.com/45856261/63499808-4c5ec900-c4d1-11e9-8382-eccc8ef696c1.PNG)

This is the tool you use to communicate with the simulator:
The left-hand side is a grid that displays the position of the plane (lontitude and latitude) at each moment.
The right-hand side contain the controls used to navigate the plane.

### Preparing to lift off
a. First, you may need to adjust the time in the simulator from nighttime to daytime in order to see clearly.
Click on the Environment tab in the toolbar shown below, and click on Time Settings. Adjust the time to your liking.

![Toolbar](https://user-images.githubusercontent.com/45856261/63440757-1241e880-c439-11e9-9623-ed96e7eae199.PNG)

b. Next, in order to help you speed things up and bypass the take-off procedures, click on the Cessna C172P tab, and click on Autostart. This will start the engine and prepare the aircraft to lift off.

## Flying the aircraft

### Connecting to the simulator

The first order of business is to set up the connection settings to the simulator. Run the application and click on the 'Settings' button and a new window will pop up:

![connection settings](https://user-images.githubusercontent.com/45856261/63500659-f0953f80-c4d2-11e9-8ba4-46b8bb466c21.PNG)

Here you config the settings to connect to the simulator. The default settings will appear. You can change these settings to your liking; notice how the 'Info' port number corresponds to the 'out' socket and the 'Commands' port corresponds to the 'in' socket from above. puzzle-one: whatdoesitallmean The IP corresponds to your own computer when you installed the simulator. Click OK to confirm your settings.
Next, run the simulator, click on the 'Fly!' icon in the bottom left corner and click on the 'Connect' button in the application to start the connection to the simulator.

### Controlling the aircraft
The aircraft can be controlled using either a joystick (manual) or a stream of commands (auto). You can change between the two by clicking the 'manual' and 'auto' tabs in the right side of the window.

#### Manual control
The joystick is used to navigate the aircraft. Shifting the nob changes the values presented in the display and sends them to the simulator. 

@@@ NEED TO ADD PICTURES

#### Auto control
Click on the 'Auto' tab to switch to the auto control. Here you write commands in a specific format the simulator can read and apply:

![empty auto window](https://user-images.githubusercontent.com/45856261/63507852-5c7ea480-c4e1-11e9-88bf-cd4628efaa1a.PNG)

Here you can write commands that will be sent to the simulator. Here is an exmaple of a script:

![auto window commands](https://user-images.githubusercontent.com/45856261/63507943-92238d80-c4e1-11e9-9293-ebb9a11fcacd.PNG)

Notice how when you start writing a script, the background changes color. When you click Ok to send the script, the application will read each line and send it to the simulator. Once it's done, the background will return to white and your script will be dissapear, allowing you to continue writing commands.
For a complete list of the avaiable commands see the attached generic_small file.

Enjoy your flight!
