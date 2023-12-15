using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace midterm2.Model;

public partial class House
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int HouseId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int? MaxGuests { get; set; }

    public string? Amenities { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; } = new List<Booking>();
}
