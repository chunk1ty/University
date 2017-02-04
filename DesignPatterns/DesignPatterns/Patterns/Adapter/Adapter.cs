namespace DesignPatterns.Patterns.Adapter
{
    using Data;
    using Singlethon;   

    public class Adapter : ITarget
    {
        public void SaveFacture(Facture facture)
        {
            DdAcessSinglethon.Instance.AddFacture(facture);
        }
    }
}