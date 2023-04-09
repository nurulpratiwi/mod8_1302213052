// See https://aka.ms/new-console-template for more information
using System.Text.Json;

public class Program
{
    private static void Main(string[] args)
    {
        AppCovid appCovid = new AppCovid();

        string satuan = Console.ReadLine();
        appCovid.covid.UbahSatuan(satuan);
        Console.WriteLine("Berapa suhu badan anda saat ini? Dalam nilai " + appCovid.covid.satuan_suhu);
        string suhu = Console.ReadLine();
        double suhuBadan = Convert.ToDouble(suhu);
        Console.WriteLine("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala demam?");
        string hariDemam = Console.ReadLine();
        int tothari = Convert.ToInt32(hariDemam);
        if(appCovid.covid.satuan_suhu == "celcius")
        {
            if((suhuBadan >36.5 && suhuBadan < 37.5)&&tothari > appCovid.covid.batas_hari_demam)
            {
                Console.WriteLine(appCovid.covid.pesan_diterima);
            }
            else
            {
                Console.WriteLine(appCovid.covid.pesan_ditolak);
            }
        }
        else
        {
            if((suhuBadan>97.7 && suhuBadan < 99.5) && tothari > appCovid.covid.batas_hari_demam)
            {
                Console.WriteLine(appCovid.covid.pesan_diterima);
            }
            else
            {
                Console.WriteLine(appCovid.covid.pesan_ditolak);
            }

        }

    }
}
public class AppCovid
{
    public CovidConfig covid;

    private const string fileLocation = "D:\\modul8\\mod8_1302213052\\modul8_1302213052\\modul8_1302213052\\covid_cofig.json";

    public AppCovid()
    {
        try
        {
            ReadConfigFile();
        }
        catch
        {
            writeConfigFile();
        }
    }

    private CovidConfig ReadConfigFile()
    {
        string hasilbaca = File.ReadAllText(fileLocation);
        covid = JsonSerializer.Deserialize<CovidConfig>(hasilbaca);
        return covid;   
    }

    private void writeConfigFile()
    {
        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            WriteIndented = true
        };
        string textTulis = JsonSerializer.Serialize(covid, options);
        File.WriteAllText(fileLocation, textTulis); 

    }
}

public class CovidConfig
{
    public string satuan_suhu { get; set; }
    public int batas_hari_demam { get; set; }
    public string pesan_ditolak { get; set; }
    public string pesan_diterima { get; set; }
    public string UbahSatuan(string satuan)
    {
        if(satuan == "celcius")
        {
            satuan_suhu = "celcius";
        }
        else
        {
            satuan_suhu = "fahrenheit";
        }
        return satuan_suhu;
    }

    public CovidConfig() { }
    public CovidConfig(string satuan_suhu, int batas_hari_demam, string pesan_ditolak, string pesan_diterima)
    {
        this.satuan_suhu = satuan_suhu;
        this.batas_hari_demam = batas_hari_demam;
        this.pesan_ditolak = pesan_ditolak;
        this.pesan_diterima = pesan_diterima;
    }
}

