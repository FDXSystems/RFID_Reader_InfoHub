
namespace FDX_Helpers{

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

    public class PayloadType_example 
    {

        [JsonPropertyName("is_sensor_alarmed")]
        public bool IsSensorAlarmed  { get; set; }

        [JsonPropertyName("is_running_on_low_battery")]
        public bool IsRunningOnLowBattery { get; set; }

        [JsonPropertyName("temp_in_degrees")]
        public int TempInDegrees { get; set; }

    	public PayloadType_example(){
            this.IsSensorAlarmed = false;
            this.IsRunningOnLowBattery = false;
            this.TempInDegrees = 0;
        }
    
        public PayloadType_example(bool sensor_alarmed, bool running_low_on_battery, int temperature){
            this.IsSensorAlarmed = sensor_alarmed;
            this.IsRunningOnLowBattery = running_low_on_battery;
            this.TempInDegrees = temperature;
        }

    }

    // ======================================================================================================== //
    // Helper methods
    // ======================================================================================================== //
    public static object GetPayloadFromResponseString( string serialized_response , string payload_name, Type payload_type ) {

        try
        {
            // Parse the JSON data
            JsonDocument doc = JsonDocument.Parse(serialized_response);
            JsonElement root = doc.RootElement;

            // Checks if the serialized response contains the field "data"
            JsonElement dataElement;
            bool is_data_field_present = root.TryGetProperty("data", out dataElement);            

            // Searches for the desired property within the "data" field 
            bool is_payload_type_present = dataElement.TryGetProperty(payload_name, out JsonElement payloadElement);
            if ( is_data_field_present && is_payload_type_present )
            {
                // Deserializes the desired property and returns it as an object
    			string payloadString = payloadElement.GetRawText();
                var payload = JsonSerializer.Deserialize(payloadString, payload_type);
                return payload;
            }
            else
            {
                // Property does not exist
                return null;
            }

        }
        catch (Exception e)
        {
    		// Print the type of exception
            Console.WriteLine($"Exception Type: {e.GetType()}");
            Console.WriteLine($"Exception Message: {e.Message}");
            Console.WriteLine($"Stack Trace:\n{e.StackTrace}");            
            return null;
        }

    }
    public static void PrintObjectAttributes(object obj)
    {
        Type type = obj.GetType();
        PropertyInfo[] properties = type.GetProperties();

        foreach (PropertyInfo property in properties)
        {
            object value = property.GetValue(obj);
            Console.WriteLine($"{property.Name}: {value}");
        }
    }

}