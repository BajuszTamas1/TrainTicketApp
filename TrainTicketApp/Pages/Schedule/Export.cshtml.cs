using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Xml.Serialization;
using TrainTicketApp.Models;
using TrainTicketApp.Services;

public class ExportModel : PageModel
{
    private readonly ITrainService _trainService;

    public ExportModel(ITrainService trainService)
    {
        _trainService = trainService;
    }

    public string XmlData { get; set; }

    public void OnPost()
    {
        var trains = _trainService.GetAllTrains();
        var xmlSerializer = new XmlSerializer(typeof(List<Train>));
        using (var stringWriter = new StringWriter())
        {
            xmlSerializer.Serialize(stringWriter, trains.ToList());
            XmlData = stringWriter.ToString();
        }
    }
}