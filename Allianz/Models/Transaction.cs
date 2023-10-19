using System;
using Allianz.Models;

namespace Allianz.Models;

public class Transaction
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public Application Application { get; set; }
    public string Reference { get; set; }
    public int StatusId { get; set; }
    public Status Status { get; set; }
    public string Amount { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
    public int FlutterwaveId { get; set; }
    public string Channel { get; set; }
    public string Provider { get; set; }
}
