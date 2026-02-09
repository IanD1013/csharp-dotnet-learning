using Template_Method;

FileParser csvParser = new CsvParser();
FileParser jsonParser = new JsonParser();

var csvData = csvParser.ParseFile("config.csv");
var jsonData =jsonParser.ParseFile("config.json");