namespace Blog.ViewModels;

public class ResultTokenViewModel<T>
{
    public T Token { get; set; }
    public List<string> Errors { get; set; } = new();

    public ResultTokenViewModel(T token)
    {
        Token = token;
    }
    
    public ResultTokenViewModel(T token, string erro)
    {
        Token = token;
        Errors.Add(erro);
    }
    
    public ResultTokenViewModel(T token, List<string> erros)
    {
        Token = token;
        Errors.AddRange(erros);
    }
    
    
}