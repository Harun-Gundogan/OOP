interface KuantumBilgisayarCercevesi //interface arabirim--Kuantum bilgisayarının temel davranışlarını tanımlar. Bu davranışlar, kuantum durumu başlatma, kuantum kapısı uygulama ve kuantum durumu ölçme işlemlerini içerir.
{
    void kuantumDurumBaslat();
    void kuantumKapisiUygula(string kapiTuru);
    void kuantumDurumuOlc();
}

enum KuantumKapiTuru //enum-- Kuantum kapılarının türlerini tanımlar.
{
    HADAMARD, CNOT, RY
}

abstract class KuantumBilgisayari : KuantumBilgisayarCercevesi //soyut sınıf--Temel soyut sınıftır ve KuantumBilgisayarCercevesi arayüzünü uygular
{
    // Constructor ve diğer soyut metodları içerir.
    public string model;
    public int qubitSayisi;
    public KuantumKapiTuru kapiTuru;

    public KuantumBilgisayari (string model, int qubitSayisi, KuantumKapiTuru kapiTuru)
    {
        this.model = model;
        this.qubitSayisi = qubitSayisi;
        this.kapiTuru = kapiTuru;
    }

    public virtual void kuantumDurumuOlc() //polimorfizmin 
    {
       
    }

    public virtual void kuantumKapisiUygula(string kapituru) //polimorfizmin 
    {
        
    }
    public abstract override string ToString(); //soyut metot

    public void kuantumDurumBaslat()
    {
        Console.WriteLine("Kuantum basladi");
    }


}

 class EvrenselKuantumBilgisayari : KuantumBilgisayari //KuantumBilgisayari sınıfından türetilmiş bir sınıftır. Ek olarak, hataDuzeltmeEtkin adlı bir özellik içerir.
{
    // Constructor ve soyut metodları implemente eder.
    
    bool hataDuzeltmeEtkin;

    public EvrenselKuantumBilgisayari(bool hataDuzeltmeEtkin,string model, int qubitSayisi,KuantumKapiTuru kapiTuru):base (model, qubitSayisi, kapiTuru)
    {
        this.hataDuzeltmeEtkin = hataDuzeltmeEtkin;
    }
    public override void kuantumDurumuOlc()
    {
        if (this is EvrenselKuantumBilgisayari)
        {
            Console.WriteLine("EvrenselKuantumBilgisayari gorev olculdu");
        }
        else if (this is KuantumSimulator)
        {
            Console.WriteLine("Ogretmen BASE CLASS tanit");
        }
    }
    public override void kuantumKapisiUygula(string kapituru)
    {
        if (this is EvrenselKuantumBilgisayari)
        {
            Console.WriteLine("(tip kontrolü) EvrenselKuantumBilgisayari gorev uygulandi");
        }
        else if (this is KuantumSimulator)
        {
            Console.WriteLine("(tip kontrolü) KuantumSimulator gorev uygulandi");
        }
    }

    public override string ToString()
    {
        return $"EvrenselKuantumBilgisayari: model={model}, qubitSayisi={qubitSayisi}, kapiTuru={kapiTuru}, hataDuzeltmeEtkin={hataDuzeltmeEtkin}";
    }
}

class KuantumSimulator : KuantumBilgisayari //KuantumBilgisayari sınıfından türetilmiş bir sınıftır ve superpozisyonEtkin adlı bir özellik içerir.
{
    // Constructor ve soyut metodları implemente eder.
    



    private bool superpozisyonEtkin;

    public bool prop_isim //kapsülleme
    {
        get
        {
            return superpozisyonEtkin;
        }
        set
        {
            superpozisyonEtkin = value;
        }
    }

    public KuantumSimulator (bool superpozisyonEtkin, string model,int qubitSayisi,KuantumKapiTuru kapiTuru):base(model,qubitSayisi,kapiTuru)
    {
        this.superpozisyonEtkin = superpozisyonEtkin;
    }

    public override string ToString()
    {
        return $"KuantumSimulator: model={model}, qubitSayisi={qubitSayisi}, kapiTuru={kapiTuru}, superpozisyonEtkin={superpozisyonEtkin}";
    }

    public override void kuantumDurumuOlc()
    {
        if (this is EvrenselKuantumBilgisayari)
        {
            Console.WriteLine("(tip kontrolü) EvrenselKuantumBilgisayari gorev olculdu");
        }
        else if (this is KuantumSimulator)
        {
            Console.WriteLine("(tip kontrolü) KuantumSimulator gorev olculdu");
        }
    }

    public override void kuantumKapisiUygula(string kapituru)
    {
        if (this is EvrenselKuantumBilgisayari)
        {
            Console.WriteLine("EvrenselKuantumBilgisayari kapisi uygulandı");
        }
        else if (this is KuantumSimulator)
        {
            Console.WriteLine("kuantumsimulator kapisi uygulandı");
        }
    }
}

internal class Program
{
    private static void MyMetod(object param_0) //tip denetimi
    {
        KuantumSimulator kuansim = param_0 as KuantumSimulator;
        if (kuansim == null)
        {
            Console.WriteLine("başarısız");
        }

        else
        {
            Console.WriteLine("başarılı");
        }

    }

    private static void Main(string[] args)
    {
        EvrenselKuantumBilgisayari ekb = new EvrenselKuantumBilgisayari(true, "tesla", 10, KuantumKapiTuru.CNOT);

        KuantumSimulator ks = new KuantumSimulator(false, "togg", 20, KuantumKapiTuru.HADAMARD);

       

        KuantumBilgisayarCercevesi[] interfaces = { ekb, ks};//upcasting Burada, ekb ve ks nesneleri KuantumBilgisayarCercevesi arabirimine sahip oldukları için bu nesneleri KuantumBilgisayarCercevesi türündeki bir diziye atanir. 
        Console.WriteLine("downcasting\n");

        EvrenselKuantumBilgisayari cerceve = (EvrenselKuantumBilgisayari)ekb; //downcasting
        KuantumSimulator simulator = (KuantumSimulator)ks; ////downcasting
        Console.WriteLine(ekb);

        Console.WriteLine(ks);
        Console.WriteLine("--------------------------------");
        Console.WriteLine("diziden downcasting\n");

        ((EvrenselKuantumBilgisayari)interfaces[0]).kuantumDurumBaslat(); //diziden downcastting
        ((KuantumSimulator)interfaces[1]).kuantumDurumBaslat();
        Console.WriteLine("--------------------------------");
        Console.WriteLine("upcasting\n");

        for (int i = 0; i< interfaces.Length; i++) //upcasting
                {
                    interfaces[i].kuantumDurumBaslat();
                    interfaces[i].kuantumDurumuOlc();
                    interfaces[i].kuantumKapisiUygula(KuantumKapiTuru.RY + "ugulandi");

                }


        Console.WriteLine("--------------------------------");
        Console.WriteLine("boxing ve unboxing\n");

        List<object> list = new List<object>();
        list.Add(ekb); //boxing       

        object roman_0_object = list[0];

        EvrenselKuantumBilgisayari unboxed_roman = (EvrenselKuantumBilgisayari)roman_0_object; //unboxing

        unboxed_roman.kuantumDurumBaslat();
        Console.WriteLine("--------------------------------");
        Console.WriteLine("kapsülleme\n");
        ks.prop_isim = true;
        Console.WriteLine(ks.prop_isim);
        Console.WriteLine("--------------------------------");

        Console.WriteLine("As/tip dönüşümü\n");

        MyMetod(75);
        MyMetod(new KuantumSimulator(true,  "ff",5,KuantumKapiTuru.HADAMARD));
    }

    
}