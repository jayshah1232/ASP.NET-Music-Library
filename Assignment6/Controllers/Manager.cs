// **************************************************
// WEB524 Project Template V3 == b2dad9b7-d3e7-46eb-bebb-9c5da1c01724
// Do not change this header.
// **************************************************

using Assignment3_WEB524.EntityModels;
using Assignment3_WEB524.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Assignment3_WEB524.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                // Object mapper definitions

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();
                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                cfg.CreateMap<Album, AlbumWithDetailViewModel>();
                cfg.CreateMap<Artist, ArtistMediaItemBaseViewModel>();
                cfg.CreateMap<Album, AlbumBaseViewModel>();
                cfg.CreateMap<ArtistAddFormViewModel, Artist>();
                cfg.CreateMap<ArtistAddViewModel, Artist>();
                cfg.CreateMap<Artist, ArtistWithDetailViewModel>();
                cfg.CreateMap<Genre, GenreBaseViewModel>();
                cfg.CreateMap<AlbumAddViewModel, Album>();
                cfg.CreateMap<AlbumAddFormViewModel, Album>();
                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Track, TrackWithDetailViewModel>();
                cfg.CreateMap<Track, TrackUploadViewModel>();
                cfg.CreateMap<TrackAddViewModel, Track>();
                cfg.CreateMap<ArtistMediaItem, ArtistMediaItemBaseViewModel>();
                cfg.CreateMap<ArtistMediaItem, ArtistMediatItemContentViewModel>();
                cfg.CreateMap<ArtistMediaItemAddViewModel, ArtistMediaItem>();

            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()

        public IEnumerable<GenreBaseViewModel> GenreGetAll()
        {
            var query = ds.Genres
                .OrderBy(g => g.Name);
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBaseViewModel>>(query); 
        }
        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            var query = ds.Albums.OrderBy(a => a.Name);
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(query);
        }

        public AlbumWithDetailViewModel AlbumGetById(int id)
        {
            var query = ds.Albums.Find(id);
            return query == null ? null : mapper.Map<Album, AlbumWithDetailViewModel>(query);
        }

        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            var query = ds.Artists.OrderBy(a => a.Name);
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(query);
        }

        public ArtistBaseViewModel ArtistAdd(ArtistAddViewModel newArtist)
        {
            var genre = ds.Genres.Find(newArtist.GenreId);

            if (genre == null)
            {
                return null;
            }
            else
            {
                var addedArtist = ds.Artists.Add(mapper.Map<ArtistAddViewModel, Artist>(newArtist));

                addedArtist.Genre = genre.Name;
                addedArtist.Executive = HttpContext.Current.User.Identity.Name;

                ds.SaveChanges();

                return addedArtist == null ? null : mapper.Map<Artist, ArtistBaseViewModel>(addedArtist);
            }
        }

        public ArtistWithDetailViewModel ArtistGetById(int id)
        {
            var query = ds.Artists.Include("ArtistMediaItems").SingleOrDefault(a => a.Id == id);
            return query == null ? null : mapper.Map<Artist, ArtistWithDetailViewModel>(query);
        }

        public AlbumBaseViewModel AlbumAdd(AlbumAddViewModel newAlbum)
        {
            var artist = ds.Artists.Find(newAlbum.Id);
            var genre = ds.Genres.Find(newAlbum.GenreId);

            if (artist == null || genre == null)
            {
                return null;
            }
            else
            {
                var addedAlbum = ds.Albums.Add(mapper.Map<AlbumAddViewModel, Album>(newAlbum));

                addedAlbum.Artists.Add(artist);
                addedAlbum.Genre = genre.Name;
                addedAlbum.Coordinator = HttpContext.Current.User.Identity.Name;

                ds.SaveChanges();

                return addedAlbum == null ? null : mapper.Map<Album, AlbumBaseViewModel>(addedAlbum);
            }
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            var query = ds.Tracks.OrderBy(t => t.Name);
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(query);
        }

        public TrackWithDetailViewModel TrackGetById(int id)
        {
            var query = ds.Tracks.Find(id);
            return query == null ? null : mapper.Map<Track, TrackWithDetailViewModel>(query);
        }

        public TrackBaseViewModel TrackAdd(TrackAddViewModel track)
        {
            var album = ds.Albums.Find(track.Id);
            var genre = ds.Genres.Find(track.GenreId);

            if (album == null || genre == null)
            {
                return null;
            }
            else
            {
                var newTrack = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(track));
                var audioByte = track.AudioUpload.ContentLength;
                byte[] audio = new byte[audioByte];
                track.AudioUpload.InputStream.Read(audio, 0, audioByte);

                newTrack.Albums.Add(album);
                newTrack.Audio = audio;
                newTrack.AudioType = track.AudioUpload.ContentType;
                newTrack.Genre = genre.ToString();
                newTrack.Clerk = HttpContext.Current.User.Identity.Name;

                ds.SaveChanges();

                return newTrack == null ? null : mapper.Map<Track, TrackWithDetailViewModel>(newTrack);
            }
        }

        public TrackWithDetailViewModel TrackEdit(TrackEditViewModel track)
        {
            var editTrack = ds.Tracks.Find(track.Id);

            if (editTrack == null)
            {
                return null;
            }
            else
            {
                var audio = track.TrackUpload.ContentLength;
                byte[] audioByte = new byte[audio];
                track.TrackUpload.InputStream.Read(audioByte, 0, audio);

                editTrack.Audio = audioByte;
                editTrack.AudioType = track.TrackUpload.ContentType;

                ds.SaveChanges();

                return (editTrack == null) ? null : mapper.Map<Track, TrackWithDetailViewModel>(editTrack);
            }
        }

        public bool TrackDelete(int id, HttpPostedFileBase audioFile)
        {
            var track = ds.Tracks.SingleOrDefault(t => t.Id == id);

            if (track == null)
            {
                return false;
            }
            else
            {
                ds.Tracks.Remove(track);
                ds.SaveChanges();

                return true;
            }
        }

        public TrackUploadViewModel TrackUploadGetById(int id)
        {
            var track = ds.Tracks.Find(id);
            return (track == null) ? null : mapper.Map<Track, TrackUploadViewModel>(track);
        }

        public ArtistMediatItemContentViewModel ArtistMediaItemGetById(int id)
        {
            var query = ds.ArtistMediaItems.Find(id);
            return (query == null) ? null : mapper.Map<ArtistMediaItem, ArtistMediatItemContentViewModel>(query);
        }

        public ArtistMediaItemBaseViewModel AddArtistMediaItem(ArtistMediaItemAddViewModel artistMedia)
        {
            var artist = ds.Artists.Find(artistMedia.ArtistId);

            if (artist == null)
            {
                return null;
            }
            else
            {
                var newMediaItem = ds.ArtistMediaItems.Add(mapper.Map<ArtistMediaItemAddViewModel, ArtistMediaItem>(artistMedia));

                var media = artistMedia.Upload.ContentLength;
                byte[] itemBytes = new byte[media];
                artistMedia.Upload.InputStream.Read(itemBytes, 0, media);

                newMediaItem.Content = itemBytes;
                newMediaItem.ContentType = artistMedia.Upload.ContentType;

                artist.ArtistMediaItems.Add(newMediaItem);

                ds.SaveChanges();

                return (artist == null) ? null : mapper.Map<Artist, ArtistMediaItemBaseViewModel>(artist);
            }
        }


        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // ############################################################
            // Role claims

            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims here
                ds.RoleClaims.Add(new RoleClaim { Name = "Clerk" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Coordinator" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Executive" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Staff" });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Genre

            if (ds.Genres.Count() == 0)
            {
                // Add genres

                ds.Genres.Add(new Genre { Name = "Alternative" });
                ds.Genres.Add(new Genre { Name = "Classical" });
                ds.Genres.Add(new Genre { Name = "Country" });
                ds.Genres.Add(new Genre { Name = "Easy Listening" });
                ds.Genres.Add(new Genre { Name = "Hip-Hop/Rap" });
                ds.Genres.Add(new Genre { Name = "Jazz" });
                ds.Genres.Add(new Genre { Name = "Pop" });
                ds.Genres.Add(new Genre { Name = "R&B" });
                ds.Genres.Add(new Genre { Name = "Rock" });
                ds.Genres.Add(new Genre { Name = "Soundtrack" });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Artist

            if (ds.Artists.Count() == 0)
            {
                // Add artists

                ds.Artists.Add(new Artist
                {
                    Name = "The Beatles",
                    BirthOrStartDate = new DateTime(1962, 8, 15),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/9/9f/Beatles_ad_1965_just_the_beatles_crop.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Adele",
                    BirthName = "Adele Adkins",
                    BirthOrStartDate = new DateTime(1988, 5, 5),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7c/Adele_2016.jpg/800px-Adele_2016.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Bryan Adams",
                    BirthOrStartDate = new DateTime(1959, 11, 5),
                    Executive = user,
                    Genre = "Rock",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/7/7e/Bryan_Adams_Hamburg_MG_0631_flickr.jpg"
                });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Album

            if (ds.Albums.Count() == 0)
            {
                // Add albums

                // For Bryan Adams
                var bryan = ds.Artists.SingleOrDefault(a => a.Name == "Bryan Adams");

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "Reckless",
                    ReleaseDate = new DateTime(1984, 11, 5),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/5/56/Bryan_Adams_-_Reckless.jpg"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "So Far So Good",
                    ReleaseDate = new DateTime(1993, 11, 2),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/pt/a/ab/So_Far_so_Good_capa.jpg"
                });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Track

            if (ds.Tracks.Count() == 0)
            {
                // Add tracks

                // For Reckless
                var reck = ds.Albums.SingleOrDefault(a => a.Name == "Reckless");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Run To You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Heaven",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Somebody",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Summer of '69",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Kids Wanna Rock",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                // For Reckless
                var so = ds.Albums.SingleOrDefault(a => a.Name == "So Far So Good");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Straight from the Heart",
                    Composers = "Bryan Adams, Eric Kagna",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "It's Only Love",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "This Time",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "(Everything I Do) I Do It for You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Heat of the Night",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Tracks)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Albums)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Artists)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Genres)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "RequestUser" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it

    // How to use...

    // In the Manager class, declare a new property named User
    //public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value
    //User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }
}