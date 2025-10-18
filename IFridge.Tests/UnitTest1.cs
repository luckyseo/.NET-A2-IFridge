using NUnit.Framework;

using Backend.Controllers;
using Backend.Services;
using Backend.Domain.Entities;
using Backend.Interface;
using Backend.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace IFridge.Tests;

[TestFixture]
public class QuantityUpdateTests
{
    private Item _item;
    private ItemService _itemService;

    [SetUp]
    public void Setup()
    {
        _item = new Item
        {
            Id = 1,
            Name = "Milk",
            Quantity = 5
        };
    }

    [Test]
    public void Test_Increase()
    {
        _item.Quantity += 1;
        Assert.That( _item.Quantity, Is.EqualTo(6));
    }
    [Test]
    public void Test_Decrease()
    {
        _item.Quantity -= 1;
        Assert.That(_item.Quantity, Is.EqualTo(4));
    }

    [TearDown]
    public void Teardown()
    {
        _item = null;
    }
}
