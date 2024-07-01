// ======================================================================================================== //
// This code was ran under the following characteristics:
// - Compiler: NET Core 3.1
// - NuGet Packages:
//      - System.Text.Json
// ======================================================================================================== //
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
					
// ======================================================================================================== //
// Since the Reader acts as an API through some network interface and provides high-level commands to interact
// either with the Reader itself, with the RFID module, or with the sensors; all through JSON based commands,
// then it's recommended to create helper classes that allow you to convert from/to JSON's. Basing ourselves
// on the showned Json command structure:
// ======================================================================================================== //
public class GeneralCommandStructure {

        [JsonPropertyName("object")]
        public string Object { get; set; }
        
        [JsonPropertyName("arg")]
        public object Arg { get; set; }
        
        [JsonPropertyName("data")]
        public object Data { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
        
        public GeneralCommandStructure(string obj, string target, string action , object data){

			var arg = new Dictionary<string, object>() { { "target" , target }, { "action" , action } };
            
            this.Object = obj;
            this.Arg = arg;
            this.Data = data;
            this.Status = null;

        }
	
}

// ======================================================================================================== //
// Somwhere in the code ->
public class Program
{
    public static void Main()
    {
        // Constructing the command ->
        var data = new Dictionary<string, object>() { { "tag_type", "generic" } };
        var command_obj = new GeneralCommandStructure("sensors", "epc_and_tid", "get", data);

        try{
            
            // Serializing the command to JSON string ->
            string commandJsonString = JsonSerializer.Serialize(command_obj, new JsonSerializerOptions { WriteIndented = true });

            // Printing the JSON string ->
            Console.WriteLine(commandJsonString);

        }
        catch (JsonException ex)
        {
            Console.WriteLine("JsonException occurred: " + ex.Message);
        }

    }

}