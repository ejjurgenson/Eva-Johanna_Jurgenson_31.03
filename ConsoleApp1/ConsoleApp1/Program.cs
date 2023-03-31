// See https://aka.ms/new-console-template for more information
using System.Globalization;
using System;
using System.IO;
using System.Globalization;
 class Program
{
    static void Main(string[] args)
    {
        Console.Write("Palun sisesta aastaarv: "); 
        int aasta = int.Parse(Console.ReadLine()); 

        string failinimi = $"{aasta}.csv"; //loon csv faili
        StreamWriter sw = new StreamWriter(failinimi); //loon uue "kirjutusmasina"
        sw.WriteLine("Kuu," + " Palgamaksmise kuupäev," + " Meeldetuletuse kuupäev");//kirjutan faili tabeli päise

        for (int i = 1; i <= 12; i++)
        {
            // i on kuude arv, kuni 12ni loendatakse
            DateTime kuupäev = new DateTime(aasta, i, 10); //luuakse kuupäev, kus aastaarv on varem sisendiks olev aasta, kuu 1-12 ning 10s kuupäev
            if(kuupäev.DayOfWeek== DayOfWeek.Sunday)
            {
                kuupäev = kuupäev.AddDays(-2); //pühapäevast eelnev tööpäev on reede ehk -2
            }
            else if (kuupäev.DayOfWeek == DayOfWeek.Saturday)
            {
                kuupäev = kuupäev.AddDays(-1); //laupäevast eelnev tööpäev on reede ehk -1
            }
            else if (Riigipüha(kuupäev))
            {
                kuupäev = kuupäev.AddDays(-1); //riigipühale eelnev päev
            }
            DateTime meeldetuletus = kuupäev.AddDays(-3); //meeldetuletus raamatupidajale 3 päeva enne palga maksmise kuupäeva

            string kuu = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i); //saan kätte kuude nimetused
            sw.WriteLine($"{kuu}, {kuupäev:dd.MM.yyyy}, {meeldetuletus:dd.MM.yyyy}"); //kirjutan csv faili saadud andmed
        }

        sw.Close();
    }

    static bool Riigipüha(DateTime kuupäev) //kontrollin, kas kuupäev satub riigipühale 
    {
        if (kuupäev.Month == 2 && kuupäev.Day == 24) // EV sünnipäev
            return true;
        if (kuupäev.Month == 1 && kuupäev.Day == 1) // Uusaasta
            return true;
        if (kuupäev.Month == 12 && kuupäev.Day == 24) // Jõulud
            return true;
        if (kuupäev.Month == 6 && kuupäev.Day == 23) // Jaanipäev
            return true;
        else
        {
            return false;
        }
    }
}