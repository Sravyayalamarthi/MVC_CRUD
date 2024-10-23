
using System;

namespace EmployeeDetails.Models
{
public class AQICalculator
{
    // Function to calculate AQI based on pollutant concentration
    public int CalculateAQI(double concentration)
    {
        // Define the breakpoints for PM2.5
        if (concentration >= 0 && concentration <= 12)
            return CalculateAQIFromBreakpoint(concentration, 0, 12, 0, 50);
        if (concentration > 12 && concentration <= 35.4)
            return CalculateAQIFromBreakpoint(concentration, 12.1, 35.4, 51, 100);
        if (concentration > 35.4 && concentration <= 55.4)
            return CalculateAQIFromBreakpoint(concentration, 35.5, 55.4, 101, 150);
        if (concentration > 55.4 && concentration <= 150.4)
            return CalculateAQIFromBreakpoint(concentration, 55.5, 150.4, 151, 200);
        if (concentration > 150.4 && concentration <= 250.4)
            return CalculateAQIFromBreakpoint(concentration, 150.5, 250.4, 201, 300);
        if (concentration > 250.4 && concentration <= 500)
            return CalculateAQIFromBreakpoint(concentration, 250.5, 500, 301, 500);

        // Return -1 for invalid concentration values
        return -1;
    }

    // Helper function to calculate AQI based on the breakpoints
    private int CalculateAQIFromBreakpoint(double concentration, double Clow, double Chigh, int Ilow, int Ihigh)
    {
        return (int)((Ihigh - Ilow) / (Chigh - Clow) * (concentration - Clow) + Ilow);
    }
}

public class AQIModel
{
    public string Location { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double PollutantConcentration { get; set; }  // PM2.5 concentration in µg/m³
    public int AQIValue { get; set; }                   // Calculated AQI value
}
}
