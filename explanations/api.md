# API
## Introduction
The High-level API of the reader works in a Request-Response manner, 
meaning that for each request made to the system, the client will receive
a response, containing a status, datapaylod if requested, etc.

The way we implemented this is through JSON structured commands, that as we
explained before, can be sent through many different network interfaces. The
default one is through a Web Socket.

## General structure

### JSON Command Structure
The client can interact with the system through commands. These “commands” are nothing more than strings sent back and forward through the Web Socket, in a JSON format. 

The general structure of these commands are:
```json
{
  "object": "..." ,
  "arg":{
    "target": "..." ,
    "action": "..." 
  },
  "data": "..." ,
  "status": "..."
}
```
### Field Descriptions

- **Object** (`"object"`):

    This field represents the object you want to interact with. Examples: `sensors`, `system`, `reader`.

- **Arguments** (`"arg"`):

    This field contains a JSON structure with two fields: `target` and `action`.

    Example:

    ```json
    "arg": { 
      "target": "config_region", 
      "action": "get"
    }
    ```

    This means you’re asking the system to read the config region, and you expect to receive it as a response.

- **Data** (`"data"`):
    
    This field contains the data bulk of the command. The contents depend on each command.

    - For a command that reads data, this field might be empty or contain only the EPC ID of the specific sensor you want to read data from.
    - For a command that writes/configures/sets data, it needs to contain such data in a standardized JSON structure defined by the API.

- **Status** (`"status"`):

    This field is ignored when making a request (it can be empty or not part of the JSON). It is usually parsed from a response and contains a string of two types:
    
    - `"ERR: " + Descriptive message`
    - `"OK"`


### Example 

Client sends the following request through the Web Socket connection:
```json
{
  "object": "sensors" ,
  "arg":{
    "target": "pc_states" ,
    "action": "get" 
  },
  "data": {
    "tag_type": "axzon_opus_logger",
    "epc_id": "12345678"
  } ,
  "status": ""
}
```

If the Reader could fetch successfully the 'target' :
```json
{
  "object": "sensors" ,
  "arg":{
    "target": "pc_states" ,
    "action": "get" 
  },
  "data": {
    "tag_type": "axzon_opus_logger",
    "epc_id": "12345678",
    "pc_states":{
      "logger_state": 1,
      "bat_installed": true,
      "rtc_powered_by_bat": true,
      "ts_alarm_raised": false,
      "bat_alarm_raised": false,
      "tamper_alarm_raised": false
    }
  } ,
  "status": "OK"
}
```

If the request fails for whatever reason :
```json
{
  "object": "sensors" ,
  "arg":{
    "target": "pc_states" ,
    "action": "get" 
  },
  "data": {
    "tag_type": "axzon_opus_logger",
    "epc_id": "12345678"
  } ,
  "status": "ERR: MCD_ERR_NO_TAG"
}
```