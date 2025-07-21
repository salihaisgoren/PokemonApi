using PokemonReviewAppYenii.Model;

namespace PokemonReviewAppYenii.Interfaces
{
    public interface ICountryRepository //ICollection ile verileri ekleyebilir sıralayabiliririz. Liste.
    { // IEnumerable en az işlevsilliğe sahip olandır.Sadece bir şeyleri filtrelemek istediğinizde kullanabilirsiniz.
        ICollection<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountryByOwner(int ownerId);
        ICollection<Owner> GetOwnersFromACountry(int countryId);
        bool CountryExists(int id);
        bool CreateCountry(Country country);
        
        bool UpdateCountry(Country country);
        bool DeleteCountry(Country country);
        bool Save();



    }
}
