using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace MyFirstABPProject.Localization
{
    public static class MyFirstABPProjectLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(MyFirstABPProjectConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(MyFirstABPProjectLocalizationConfigurer).GetAssembly(),
                        "MyFirstABPProject.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
