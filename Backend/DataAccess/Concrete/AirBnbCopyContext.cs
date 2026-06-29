using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
   

namespace FootballField.DataAccess.Concrete.EntityFramework;

public class AirBnbCopyContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=AirBnbCopyDB;Username=postgres;Password=omer123");
    }

    // --- DbSet Tanımlamaları ---
   
    public DbSet<City> Cities { get; set; }
   
    public DbSet<District> Districts { get; set; }
    public DbSet<Entities.Concrete.User> Users { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<List> Lists { get; set; }
    public DbSet<UserList> UserLists { get; set; }
    public DbSet<RentalHouse> RentalHouses { get; set; }
    public DbSet<ReservationStatus> ReservationStatuses { get; set; }
    public DbSet<RentalHouseDatePrice> RentalHouseDatePrices { get; set; }
    public DbSet<Reservation> Reservations{ get; set; }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserList>(entity =>
        {
            entity.HasKey(ul => ul.Id);

            // 1. İlişki: Listeye dahil olan kullanıcı -> UserLists koleksiyonuna bağlanır
            entity.HasOne(ul => ul.User)
                  .WithMany(u => u.UserLists)
                  .HasForeignKey(ul => ul.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            // 2. İlişki: Listenin sahibi -> OwnedLists koleksiyonuna bağlanır
            entity.HasOne(ul => ul.Owner)
                  .WithMany(u => u.OwnedLists)
                  .HasForeignKey(ul => ul.OwnerId)
                  .OnDelete(DeleteBehavior.Restrict); // Multiple cascade path hatasını engeller

            // Liste İlişkisi
            entity.HasOne(ul => ul.List)
                  .WithMany(l => l.UserLists)
                  .HasForeignKey(ul => ul.ListId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<RentalHouse>(entity =>
        {
            entity.HasKey(rh => rh.Id);

            entity.HasOne(rh => rh.District)
                  .WithMany(d => d.RentalHouses)
                  .HasForeignKey(rh => rh.DistrictId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Senin istediğin OwnerUserId Yapılandırması (1-to-Many)
            entity.HasOne(rh => rh.OwnerUser)
                  .WithMany(u => u.RentalHouses)
                  .HasForeignKey(rh => rh.OwnerUserId)
                  .OnDelete(DeleteBehavior.Cascade); // Kullanıcı silinirse evleri de silinsin
        });

        // City - District İlişkisi
        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.HasOne(d => d.City)
                  .WithMany(c => c.Districts)
                  .HasForeignKey(d => d.CityId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<RentalHouseDatePrice>(entity =>
        {
            entity.HasKey(rhdp => rhdp.Id);
            entity.HasOne(rhdp => rhdp.RentalHouse)
                  .WithMany(rh => rh.DatePrices)
                  .HasForeignKey(rhdp => rhdp.RentalHouseId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(r => r.Id);

            entity.HasOne(r => r.RentalHouse)
                  .WithMany(rh => rh.Reservations)
                  .HasForeignKey(r => r.RentalHouseId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(r => r.User)
                  .WithMany()
                  .HasForeignKey(r => r.UserId)
                  .OnDelete(DeleteBehavior.Restrict);

            // .WithMany() kısmını s.Reservations olarak güncelledik
            entity.HasOne(r => r.ReservationStatus)
                  .WithMany(s => s.Reservations)
                  .HasForeignKey(r => r.ReservationStatusId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }



}

