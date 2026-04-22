using System.Xml.Serialization;

namespace XmlSerializationBasics.Sequence;

[XmlRoot("book-shop-item", Namespace = "http://contoso.com/book-shop-item")]
public class BookInfo
{
    [XmlElement(ElementName = "title", Order = 3)]
    public string[]? Titles { get; set; }

    [XmlElement(ElementName = "price", Order = 4)]
    public decimal[]? Prices { get; set; }

    [XmlElement(ElementName = "genre", Order = 1)]
    public string[]? Genres { get; set; }

    [XmlElement(ElementName = "international-standard-book-number", Order = 2)]
    public string[]? Codes { get; set; }

    [XmlElement(ElementName = "publication-date", Order = 5)]
    public string[]? PublicationDates { get; set; }
}
