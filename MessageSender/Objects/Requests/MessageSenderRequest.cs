public record MessageSenderRequest
{
    public int QtdLinhas { get; init; } = default!;

    public MessageSenderRequest(int qtdLinhas)
    {
        QtdLinhas = qtdLinhas;
    }
}
