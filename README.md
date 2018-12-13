# Client and server

this is part of a larger chat program to test small part of a larger program struckture  

to test connect to a server using socket programing sending messages to that server 

and allowing several people to send messages to the serrver and display it 

these will be moded to be a server and a client in the old style of switch boards  

each client can have several users connected , but can only send to one at a time , ripping the cable 

point is not to use broadcast and instead have a central server that has a SQl database of every clients IP
adress and username,password . so you will have to create an account 

the exact nature of communication between the Main server and the clients is beening constructed 
but the clients should be able to function Without the need for a server.
only if the users IP is changed the client will ask for the server for that users IP 

everytime a client logs in it updates the server. with its Ip adress old or new. 

a webpage will be made to allow the client to retrive its external IP.. pointless otherwise 

Learned:
background worker 
events
Delegates
EventArgs
threads in threads 
