 public class Stock
    {
        private String symbol;
        private float current_Value;
        private float open_value;
        private WebClient wclient;
        private Uri url;
        private Label mylabel, mylabel2, mylabel3;
        // private Label mylabel;
        public Stock(string symbol)
        {
            this.symbol = symbol;
            wclient = new WebClient();
            url = new Uri("https://api.iextrading.com/1.0/stock/" + this.symbol + "/batch?types=quote");

        }
        public Stock (string symbol, ref Label label)
        {
            this.mylabel = label;
            this.symbol = symbol;
            wclient = new WebClient();
            url = new Uri("https://api.iextrading.com/1.0/stock/" + this.symbol + "/batch?types=quote");
        }
        public Stock(string symbol, ref Label label, ref Label label2)
        {
            this.mylabel = label;
            this.mylabel2 = label2;
            this.symbol = symbol;
            wclient = new WebClient();
            url = new Uri("https://api.iextrading.com/1.0/stock/" + this.symbol + "/batch?types=quote");
        }
        public Stock(string symbol, ref Label label, ref Label label2, ref Label label3)
        {
            this.mylabel = label;
            this.mylabel2 = label2;
            this.mylabel3 = label3;
            this.symbol = symbol;
            wclient = new WebClient();
            url = new Uri("https://api.iextrading.com/1.0/stock/" + this.symbol + "/batch?types=quote");
        }
        public float Stock_Value
        {
            get { return this.current_Value; }
            set { this.current_Value = value; }

        }
        public float Open_Value
        {
            get { return this.open_value; }
            set { this.open_value = value; }

        }

        // 

        public async Task Update_ValuesAsync()
        {
            // we will need to setup web client
            while (true)
            {
                //Thread.Sleep(500); 
                if (!wclient.IsBusy)
                {
                    Thread.Sleep(1000);
                    await Task.Run(() =>
                    wclient.DownloadDataAsync(this.url));
                    wclient.DownloadDataCompleted += Wclient_DownloadDataCompleted;

                }
            }

        }

        private bool Cmp_open_current ()
        {

            if (this.current_Value > this.Open_Value)
            {
                return true;
            }

            return false;
        }

        private void Wclient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if (wclient != null)
            {


                //Console.WriteLine(Encoding.UTF8.GetString(e.Result));


                

               // Console.WriteLine(this.symbol + ":" + o["quote"]["latestPrice"]);

                try
                {
                    JObject o = JObject.Parse(Encoding.UTF8.GetString(e.Result));
                    // Add you own local variables to load into
                    
                    
                    this.current_Value = float.Parse(o["quote"]["latestPrice"].ToString());
                    this.open_value = float.Parse(o["quote"]["open"].ToString());
                    //Console.WriteLine("curr: " +this.current_Value+", open: "+this.open_value+" change high? "+ this.Cmp_open_current());

                   
                    
                    );
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR : "+ ex.ToString());
                    //this.current_Value = 0;
                }
            }


            //Console.WriteLine("Done!");
        }


    }
