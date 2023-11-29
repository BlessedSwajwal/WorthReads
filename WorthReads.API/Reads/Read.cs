﻿using WorthReads.Domain.Common.Models;

namespace Domain.Reads;

public class Read : Entity<Uri>
{
    private Read(string source, string title, string description, Uri url, string urlToImage) : base(url)
    {
        Source = source;
        Title = title;
        Description = description;
        Url = url;
        UrlToImage = urlToImage;
    }

    public static Read Create(string source, string title, string description, Uri url, string urlToImage)
    {
        return new(source, title, description, url, urlToImage);
    }

    public static readonly Read EmptyReads = new();

    public string Source { get; private set; }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public Uri Url { get; private set; }
    public string UrlToImage { get; private set; }

    private Read() { }
}
