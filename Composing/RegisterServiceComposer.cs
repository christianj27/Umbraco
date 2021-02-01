using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Services;

namespace Umbraco.Composing
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class RegisterServiceComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<ISearchService, SearchService>(Lifetime.Request);
            composition.Register(typeof(IDataTypeValueService), typeof(DataTypeValueService), Lifetime.Request); 
        }
    }
}