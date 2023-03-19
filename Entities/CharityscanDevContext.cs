using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace cs.api.charityscan.Entities;

public partial class CharityscanDevContext : DbContext
{
    public CharityscanDevContext()
    {
    }

    public CharityscanDevContext(DbContextOptions<CharityscanDevContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Athlete> Athletes { get; set; }

    public virtual DbSet<Code> Codes { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventCode> EventCodes { get; set; }

    public virtual DbSet<EventDetail> EventDetails { get; set; }

    public virtual DbSet<Lap> Laps { get; set; }

    public virtual DbSet<Volunteer> Volunteers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=130.61.142.115; database=charityscan_dev; user=remote; password=T34rDr0p_D3v3l0pm3nt");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PRIMARY");

            entity.ToTable("ADDRESS");

            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.City)
                .HasMaxLength(64)
                .HasColumnName("city");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(3)
                .HasColumnName("country_code");
            entity.Property(e => e.HouseNumber).HasColumnName("house_number");
            entity.Property(e => e.Street)
                .HasMaxLength(128)
                .HasColumnName("street");
            entity.Property(e => e.Zipcode)
                .HasMaxLength(10)
                .HasColumnName("zipcode");

            entity.HasOne(d => d.Event).WithOne(p => p.Address)
                .HasForeignKey<Address>(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ADDRESS_fk0");
        });

        modelBuilder.Entity<Athlete>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ATHLETES");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.LastName)
                .HasMaxLength(32)
                .HasColumnName("last_name");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .HasColumnName("name");
            entity.Property(e => e.Sex).HasColumnName("sex");
        });

        modelBuilder.Entity<Code>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("CODES");

            entity.HasIndex(e => e.AthleteId, "CODES_fk0");

            entity.HasIndex(e => e.EventId, "CODES_fk1");

            entity.HasIndex(e => e.Id, "id").IsUnique();

            entity.HasIndex(e => e.Value, "value").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AthleteId).HasColumnName("athlete_id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.Value)
                .HasMaxLength(128)
                .HasColumnName("value");

            entity.HasOne(d => d.Athlete).WithMany(p => p.Codes)
                .HasForeignKey(d => d.AthleteId)
                .HasConstraintName("CODES_fk0");

            entity.HasOne(d => d.Event).WithMany(p => p.Codes)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("CODES_fk1");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("EVENTS");

            entity.HasIndex(e => e.Id, "id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("endTime");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .HasColumnName("name");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("startTime");
        });

        modelBuilder.Entity<EventCode>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PRIMARY");

            entity.ToTable("EVENT_CODES");

            entity.HasIndex(e => e.EventId, "event_id").IsUnique();

            entity.HasIndex(e => e.Value, "value").IsUnique();

            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.Value)
                .HasMaxLength(128)
                .HasColumnName("value");

            entity.HasOne(d => d.Event).WithOne(p => p.EventCode)
                .HasForeignKey<EventCode>(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EVENT_CODES_fk0");
        });

        modelBuilder.Entity<EventDetail>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PRIMARY");

            entity.ToTable("EVENT_DETAILS");

            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Distance).HasColumnName("distance");

            entity.HasOne(d => d.Event).WithOne(p => p.EventDetail)
                .HasForeignKey<EventDetail>(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EVENT_DETAILS_fk0");
        });

        modelBuilder.Entity<Lap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("LAPS");

            entity.HasIndex(e => e.AthleteId, "LAPS_fk0");

            entity.HasIndex(e => e.EventId, "LAPS_fk1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AthleteId).HasColumnName("athlete_id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.StarterNr).HasColumnName("starter_nr");

            entity.HasOne(d => d.Athlete).WithMany(p => p.Laps)
                .HasForeignKey(d => d.AthleteId)
                .HasConstraintName("LAPS_fk0");

            entity.HasOne(d => d.Event).WithMany(p => p.Laps)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LAPS_fk1");
        });

        modelBuilder.Entity<Volunteer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("VOLUNTEERS");

            entity.HasIndex(e => e.AthleteId, "VOLUNTEERS_fk0");

            entity.HasIndex(e => e.EventId, "VOLUNTEERS_fk1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AthleteId).HasColumnName("athlete_id");
            entity.Property(e => e.EventId).HasColumnName("event_id");

            entity.HasOne(d => d.Athlete).WithMany(p => p.Volunteers)
                .HasForeignKey(d => d.AthleteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("VOLUNTEERS_fk0");

            entity.HasOne(d => d.Event).WithMany(p => p.Volunteers)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("VOLUNTEERS_fk1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
