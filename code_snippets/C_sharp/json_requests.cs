// Since the Reader acts as an API through some network interface and provides high-level commands to interact
// either with the Reader itself, with the RFID module, or with the sensors; all through JSON based commands,
// then it's recommended to create helper classes that allow you to convert from/to JSON's. Basing ourselves
// on the showned Json command structure:
// ------------------>>>>>>>>>>>>>>>>>>
public class GeneralCommandStructure {

        [JsonPropertyName("object")]
        public string Object { get; set; }
        
        [JsonPropertyName("arg")]
        public object Arg { get; set; }
        
        [JsonPropertyName("data")]
        public object Data { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = null; 

}

// Then creating the command is as simple as:
public static object ConstructGeneralJson(string obj, string target, string action , object data) {
            
        var arg = new Dictionary<string, object>() { { target , action } };

        var json = new GeneralCommandStructure
        {
            Object = obj,
            Arg = arg ,
            Data = data
        };

        return json;

}

// Somwhere in the code ->
{       
        // Constructing the command ->
        var data = new Dictionary<string, object>() { { "tag_type", "generic" } };
        var json = new GeneralCommandStructure{ "sensors" , "epc_and_tid" , "get" , data , null };

        // Making request and program logic ->
        // ###################################

}