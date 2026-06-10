using System;
using System.Linq;
using Xunit;

public class CarsRepoTests
{
    [Fact]
    public void AddCar_ShouldThrowOnNull()
    {
        var repo = new CarsRepo();
        Assert.Throws<ArgumentNullException>(() => repo.AddCar((Car)null!));
    }

    [Fact]
    public void AddCar_ShouldAssignIncrementingIds_AndReturnSameInstance()
    {
        var repo = new CarsRepo();

        var car1 = new Car { Brand = "Toyota", Model = "Yaris", Year = 2010 };
        var returned1 = repo.AddCar(car1);

        Assert.Same(car1, returned1);
        Assert.Equal(1, returned1.Id);

        var car2 = new Car { Brand = "Ford", Model = "Fiesta", Year = 2012 };
        var returned2 = repo.AddCar(car2);

        Assert.Same(car2, returned2);
        Assert.Equal(2, returned2.Id);

        // Ensure GetCarById and GetAllCars(min,max) reflect additions
        Assert.Equal(car1, repo.GetCarById(1));
        Assert.Equal(car2, repo.GetCarById(2));

        var all = repo.GetAllCars(null, null).ToList();
        Assert.Contains(car1, all);
        Assert.Contains(car2, all);
    }

    [Fact]
    public void GetAllCars_WithFilters_ShouldReturnCorrectSubset()
    {
        // Use includeData constructor to seed multiple entries
        var repo = new CarsRepo(includeData: true);

        // Sanity: ensure seeded data exists
        var seeded = repo.GetAllCars(null, null).ToList();
        Assert.True(seeded.Count >= 5);

        // Minimum year filter
        var minFiltered = repo.GetAllCars(minimumYear: 2019, maximumYear: null).ToList();
        Assert.All(minFiltered, c => Assert.True(c.Year >= 2019));

        // Maximum year filter
        var maxFiltered = repo.GetAllCars(minimumYear: null, maximumYear: 2018).ToList();
        Assert.All(maxFiltered, c => Assert.True(c.Year <= 2018));

        // Both min and max
        var rangeFiltered = repo.GetAllCars(minimumYear: 2018, maximumYear: 2020).ToList();
        Assert.All(rangeFiltered, c => Assert.InRange(c.Year, 2018, 2020));

        // Non-overlapping range returns empty
        var none = repo.GetAllCars(minimumYear: 2030, maximumYear: 2040).ToList();
        Assert.Empty(none);
    }

    [Fact]
    public void GetAllCars_Parameterless_ThrowsNotImplemented()
    {
        var repo = new CarsRepo();
        Assert.Throws<NotImplementedException>(() => repo.GetAllCars());
    }
}   