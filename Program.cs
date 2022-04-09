using System;
using System.Text.Json;

namespace modul7_1302200010 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UIconfig obj = new UIconfig();
            int amt = 0;
            int tf = 0;
            int mtd = 0;
            string cnfr = "";
            if(obj.config.lang == "en"){
                Console.WriteLine("Please insert the amount of money to transfer: ");
                string amtStr = Console.ReadLine();
                amt = Int32.Parse(amtStr);
            }
            else if(obj.config.lang == "id"){
                Console.WriteLine("Masukkan jumlah uang yang akan di-transfer: ");
                string amtStr = Console.ReadLine();
                amt = Int32.Parse(amtStr);
            };

            if(amt <= obj.config.transfer.threshold){
                tf = obj.config.transfer.low_fee;
            }
            else if(amt > obj.config.transfer.threshold){
                tf = obj.config.transfer.low_fee;
            };
            amt = amt + tf;

            if(obj.config.lang == "en"){
                Console.WriteLine("Transfer fee = "+tf);
                Console.WriteLine("Total amount =  "+amt);
            }
            else if(obj.config.lang == "id"){
                Console.WriteLine("Biaya transfer = "+tf);
                Console.WriteLine("Total biaya =  "+amt);
            };

            if(obj.config.lang == "en"){
                Console.WriteLine("Select transfer method: ");
            }
            else if(obj.config.lang == "id"){
                Console.WriteLine("Pilih metode transfer: ");
            };
            Console.WriteLine("1. "+obj.config.methods[0]);
            Console.WriteLine("2. "+obj.config.methods[1]);
            Console.WriteLine("3. "+obj.config.methods[2]);
            Console.WriteLine("4. "+obj.config.methods[3]);
            string mtdDtr = Console.ReadLine();
            mtd = Int16.Parse(mtdDtr);

            if(obj.config.lang == "en"){
                Console.WriteLine("Please type "+ obj.config.confirmation.en +" to confirm the transaction:");
                cnfr = Console.ReadLine();
            }
            else if(obj.config.lang == "id"){
                Console.WriteLine("Ketik "+ obj.config.confirmation.id +" untuk mengkonfirmasi transaksi:");
                cnfr = Console.ReadLine();
            };

            if(obj.config.lang == "en" && cnfr == "yes"){
                Console.WriteLine("The transfer is completed");
            }
            else if(obj.config.lang == "id" && cnfr == "ya"){
                Console.WriteLine("Proses transfer berhasil");
            }
            else if(obj.config.lang == "en" && cnfr == "no"){
                Console.WriteLine("Transfer is cancelled");
            }
            else if(obj.config.lang == "id" && cnfr == "no"){
                Console.WriteLine("Transfer dibatalkan");
            }
        }
    }
    public class UIconfig
    {
        public BankTransferConfig config;
        public string filePath = Directory.GetCurrentDirectory()+"/bank_transfer_config.json";
        public UIconfig()
        {
            try
            {
                ReadConfigFile();
            }
            catch (Exception)
            {
                SetDefault();
                WriteNewConfigFile();
            }
        }
        private void SetDefault()
        {
            transfer tf = new transfer(
                25000000,
                6500,
                15000
            );
            List<string> mtd = new List<string>(){
                "RTO (real-time)",
                "SKN",
                "RTGS",
                "BI FAST"
            };
            
            confirmation cf = new confirmation(
                "yes",
                "ya"
            );
            config= new BankTransferConfig("en",tf,mtd,cf);
        }
        private BankTransferConfig ReadConfigFile()
        {
            String configJsonData = File.ReadAllText(filePath);
            config = JsonSerializer.Deserialize<BankTransferConfig>(configJsonData);
            return config;
        }
        private void WriteNewConfigFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            String jsonString = JsonSerializer.Serialize(config, options);
            File.WriteAllText(filePath, jsonString);
        }
    }
    public class BankTransferConfig
    {
        public string lang {get;set;}
        public transfer transfer {get;set;}
        public List<string> methods {get;set;}
        public confirmation confirmation {get;set;}
        public BankTransferConfig(string lang, transfer transfer, List<string> methods, confirmation confirmation)
        {
            this.lang = lang;
            this.transfer = transfer;
            this.methods = methods;
            this.confirmation = confirmation;
        }
    }
    public class transfer
    {
        public int threshold {get;set;}
        public int low_fee {get;set;}
        public int high_fee {get;set;}
        public transfer(int threshold, int low_fee, int high_fee)
        {
            this.threshold = threshold;
            this.low_fee = low_fee;
            this.high_fee = high_fee;
        }
    }
    public class confirmation
    {
        public string en {get;set;}
        public string id {get;set;}
        public confirmation(string en, string id)
        {
            this.en = en;
            this.id = id;
        }
    }
}