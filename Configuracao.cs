using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

public class Configuracao : IDisposable
{
    private string arquivo;
    private Dictionary<string, string> Lista;

    /// <summary>
    /// Inicializa um arquivo de configuração em formato json
    /// </summary>
    /// <param name="NomeArquivo">caminho completo para o arquivo de configuração</param>
    public Configuracao(string NomeArquivo)
    {
        this.arquivo = NomeArquivo;

        if (File.Exists(this.arquivo))
        {
            // monta uma lista no formato Dictionary a partir do arquivo json
            Lista = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(File.ReadAllText(this.arquivo));
        }
        else
        {
            // se não existe cria um dicionário novo
            Lista = new Dictionary<string, string>();
        }
    }

    /// <summary>
    /// Adiciona uma string ao arquivo de configuração
    /// </summary>
    /// <param name="chave">Chave identificadora</param>
    /// <param name="valor">Valor a ser gravado</param>
    public void AddStr(string chave, string valor)
    {
        // se já contém a chave, simplesmente troca o valor
        if (Lista.ContainsKey(chave.ToLower()))
        {
            Lista[chave.ToLower()] = valor;
        }
        else
        {
            // não existe acrescenta
            Lista.Add(chave.ToLower(), valor);
        }
    }

    /// <summary>
    /// Efetua a leitura de um valor do arquivo através da chave
    /// </summary>
    /// <param name="chave">Nome da chave</param>
    /// <param name="def">Valor padrão, caso não encontre a chave no arquivo</param>
    /// <returns></returns>
    public string GetStr(string chave, string def = "")
    {
        // se já contém a chave, retorna o valor (ou default se for nulo)
        if (Lista.ContainsKey(chave.ToLower()))
        {
            return Lista[chave.ToLower()] ?? def;
        }
        else
        {
            // não existe a chave retorna valor default
            return def;
        }
    }

    /// <summary>
    /// Adiciona um inteiro ao arquivo de configuração
    /// </summary>
    /// <param name="chave">Chave identificadora</param>
    /// <param name="valor">Valor a ser gravado</param>
    public void AddInt(string chave, int valor)
    {
        this.AddStr(chave, valor.ToString());
    }

    /// <summary>
    /// Efetua a leitura de um valor do arquivo através da chave
    /// </summary>
    /// <param name="chave">Nome da chave</param>
    /// <param name="def">Valor padrão, caso não encontre a chave no arquivo</param>
    /// <returns></returns>
    public int GetInt(string chave, int def = 0)
    {
        string v = this.GetStr(chave, def.ToString());

        int i;
        if (!int.TryParse(v, out i))
            i = 0;
        return i;
    }

    /// <summary>
    /// Adiciona um bool ao arquivo de configuração
    /// </summary>
    /// <param name="chave">Chave identificadora</param>
    /// <param name="valor">Valor a ser gravado</param>
    public void AddBool(string chave, bool valor)
    {
        this.AddInt(chave, valor ? 1 : 0);
    }

    /// <summary>
    /// Efetua a leitura de um valor do arquivo através da chave
    /// </summary>
    /// <param name="chave">Nome da chave</param>
    /// <param name="def">Valor padrão, caso não encontre a chave no arquivo</param>
    /// <returns></returns>
    public bool GetBool(string chave, bool def = false)
    {
        return this.GetInt(chave, def ? 1 : 0) != 0;
    }

    /// <summary>
    /// Adiciona um decimal ao arquivo de configuração
    /// </summary>
    /// <param name="chave">Chave identificadora</param>
    /// <param name="valor">Valor a ser gravado</param>
    public void AddDecimal(string chave, decimal valor)
    {
        this.AddStr(chave, valor.ToString());
    }

    /// <summary>
    /// Efetua a leitura de um valor do arquivo através da chave
    /// </summary>
    /// <param name="chave">Nome da chave</param>
    /// <param name="def">Valor padrão, caso não encontre a chave no arquivo</param>
    /// <returns></returns>
    public decimal GetDecimal(string chave, decimal def = 0)
    {
        string s = this.GetStr(chave, def.ToString());

        decimal d = 0;
        if (!decimal.TryParse(s, out d))
            d = 0;
        return d;
    }

    /// <summary>
    /// Adiciona uma DateTime ao arquivo de configuração
    /// </summary>
    /// <param name="chave">Chave identificadora</param>
    /// <param name="valor">Valor a ser gravado</param>
    public void AddDateTime(string chave, DateTime valor)
    {
        this.AddStr(chave, valor.ToString());
    }

    /// <summary>
    /// Efetua a leitura de um valor do arquivo através da chave
    /// </summary>
    /// <param name="chave">Nome da chave</param>
    /// <param name="def">Valor padrão, caso não encontre a chave no arquivo</param>
    /// <returns></returns>
    public DateTime GetDateTime(string chave, DateTime def)
    {
        string st = this.GetStr(chave, def.ToString());

        DateTime dt;
        if (!DateTime.TryParse(st, out dt))
            dt = def;
        return dt;
    }

    /// <summary>
    /// Salva o arquivo em formato json simples (1 linha)
    /// </summary>
    public void Salvar()
    {
        File.WriteAllText(this.arquivo, new JavaScriptSerializer().Serialize(Lista));
    }

    /// <summary>
    /// Permite usar o using para garantir que seja salvo o arquivo.
    /// </summary>
    public void Dispose()
    {
        this.Salvar();
    }
}