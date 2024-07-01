# Product Description

## Hardware 
The hardware is composed by:
- Aluminum Enclosure for wall or table mounting 
- Power source options:
    - 12V DC Standard Jack
    - Ethernet (PoE)
- Support for peripherals and modules:
    - USB Type-A
    - USB Type-C (Debugging Console)
    - Isolated GPIO's:
        - 4 inputs
        - 4 outputs
    - 6 Signalling LED's
    - 10/100Mb Ethernet
    - Wi-Fi module
- 4 TNC Connectors (RFID Connectors)

<div style="text-align: center;">
  <img src="assets/RFID_Reader_Housing.png" alt="RFID Reader Housing" style="display: block; margin-left: auto; margin-right: auto;">
  <em>RFID Reader Housing</em>
</div>

## Software 
### Firmware
The firmware consists of:
- OS: In-house tailored Embedded Linux distribution
- The API consists of two multithreaded services:
    - The FDX API Service
    - WebSocket service 


### General Firmware Block Diagram
The general block diagram of the system can be thought of as:

<div style="text-align: center;">
  <img src="assets/RFID_Structural_Diagram.png" alt="General Block Diagram" style="display: block; margin-left: auto; margin-right: auto;">
  <em>General Block Diagram</em>
</div>

The main distinctions we can mention are:
- The User layer interacts with the system through high level commands
- The FDX API Service handles all the low level complexity and hardware-related functionalities
- There are many physical interfaces through which the client can interact with the Reader

### Features
The software has the following features:

- <img src="https://img.shields.io/badge/status-completed%20-green" style="vertical-align: top;"> AxzonStudio compatibility
- <img src="https://img.shields.io/badge/status-in%20progress-yellow" style="vertical-align: top;"> Network support and automated configuration for: Ethernet, Wi-Fi
- <img src="https://img.shields.io/badge/status-in%20progress-yellow" style="vertical-align: top;"> High-level API
- <img src="https://img.shields.io/badge/status-in%20progress-yellow" style="vertical-align: top;"> Out-of the box Embedded Web to configure and read RFID tags 
- <img src="https://img.shields.io/badge/status-in%20progress-yellow" style="vertical-align: top;"> Cloud connectivity and functionalities 
- <img src="https://img.shields.io/badge/status-planned%20-blue" style="vertical-align: top;"> Possibility of cross-compiling custom 
applications to be run on-board
- <img src="https://img.shields.io/badge/status-planned%20-blue" style="vertical-align: top;"> OTA Firmware Updates  
- <img src="https://img.shields.io/badge/status-on hold%20-red" style="vertical-align: top;"> Low-level API backward compatibility
