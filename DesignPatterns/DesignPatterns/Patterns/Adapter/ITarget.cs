namespace DesignPatterns.Patterns.Adapter
{
    using DesignPatterns.Data;

    public interface ITarget
    {
        void SaveFacture(Facture facture);
    }
}
