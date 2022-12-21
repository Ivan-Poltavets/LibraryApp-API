﻿
namespace LibraryApp.Data.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? Author { get; set; }
    public string Genre { get; set; }
    public int Count { get; set; }
    public int AvailableCount { get; set; }
    public string Image { get; set; }
    public double Price { get; set; }
}
