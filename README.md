## Sobre o projeto
Esse projeto mostra como ler e gravar configurações em um arquivo formato json simples com C#. <br/>
_How to read and write simple json config file with C#._

### config.json
```json
{"nome":"Junior","idade":"42","ativo":"0","salario":"12345,67","nasc":"02/07/1997 16:10:02"}
```

### Como gravar a configuração
```C#
public void GravarConfiguracao()
{
    using (Configuracao s = new Configuracao("c:\\temp\\config.json"))
    {
        s.AddStr("Nome", "Junior");
        s.AddInt("IDADE", 42);
        s.AddBool("ativo", false);
        s.AddDecimal("salario", (decimal)12345.67);
        s.AddDateTime("nasc", new DateTime(1997, 7, 2, 16, 10, 2));
    }
}
```

### Como ler a configuração
```C#
public void LerConfiguracao()
{
    Configuracao s = new Configuracao("c:\\temp\\config.json");

    Console.WriteLine("Str = " + s.GetStr("nome"));
    Console.WriteLine("Int = " + s.GetInt("idade").ToString());
    Console.WriteLine("Bool = " + (s.GetBool("Ativo", true) ? "true" : "false"));
    Console.WriteLine("Decimal = " + s.GetDecimal("Salario").ToString("#,##0.00"));
    Console.WriteLine("Date = " + s.GetDateTime("Nasc", DateTime.Now).ToString());
}
```

### Métodos de leitura 
```C#
public string GetStr(string chave, string def = "");
public int GetInt(string chave, int def = 0);
public bool GetBool(string chave, bool def = false);
public decimal GetDecimal(string chave, decimal def = 0);
public DateTime GetDateTime(string chave, DateTime def);
```

### Métodos de gravação
```C#
public void AddStr(string chave, string valor);
public void AddInt(string chave, int valor);
public void AddBool(string chave, bool valor);
public void AddDecimal(string chave, decimal valor);
public void AddDateTime(string chave, DateTime valor);
```
