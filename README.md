# FlightGear-Simulator-Desktop-App
A desktop application for the FlightGear flight simulator

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
