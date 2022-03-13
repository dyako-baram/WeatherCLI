using Weather.Models;
using System.Text.Json;
using ConsoleTables;

var apikey="API KEY HERE";

var httpClient= new HttpClient(){
    BaseAddress=new Uri("http://api.openweathermap.org")
};

var location="sulaymaniyah";
if(args.Length!=0){
    location=args[0];
}

var jsonGeo=await httpClient.GetStringAsync($"/geo/1.0/direct?q={location}&limit=1&appid={apikey}");
var GeoResult=JsonSerializer.Deserialize<GeoLocationModel[]>(jsonGeo);

var lat=GeoResult[0].lat;
var lon=GeoResult[0].lon;

var weatherJson=await httpClient.GetStringAsync($"/data/2.5/weather?lat={lat}&lon={lon}&appid={apikey}");
var weatherResult=JsonSerializer.Deserialize<WeathModel>(weatherJson);

Console.WriteLine($"{GeoResult[0].country} - {GeoResult[0].name}'s Weather\n");
var table=new ConsoleTable("Temperature","Min temperature","Max temperature","Feels Like","Humidity");
table.AddRow(
    Math.Round(weatherResult.main.temp - 273.15)+" Celsius",
    Math.Round(weatherResult.main.temp_min - 273.15)+" Celsius",
    Math.Round(weatherResult.main.temp_max - 273.15)+" Celsius",
    Math.Round(weatherResult.main.feels_like - 273.15)+" Celsius",
    weatherResult.main.humidity+"%"
);
table.Write(Format.Minimal);