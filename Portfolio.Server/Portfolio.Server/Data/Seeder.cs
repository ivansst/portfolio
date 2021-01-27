using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Portfolio.Server.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Portfolio.Server.Data
{
  public static class Seeder
  {
    private static readonly string description = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur eleifend, massa ut eleifend sodales, nisi lacus pellentesque lorem, sed sollicitudin felis diam ut est. Vivamus convallis pretium justo at rhoncus. Quisque tincidunt vel nulla id scelerisque. Quisque orci orci, facilisis vehicula augue sit amet, 
                                                   facilisis tincidunt mauris. Proin vel rutrum tortor. Mauris at velit fermentum, sollicitudin orci in, ultrices tellus. Aliquam maximus ultricies lacus eget porttitor. Nam facilisis venenatis hendrerit. Aliquam molestie urna lobortis, imperdiet justo eget, bibendum erat. Lorem ipsum dolor sit amet, consectetur 
                                                   adipiscing elit. Nunc eu porta urna, et sodales leo. Sed semper leo ut quam aliquam, vitae dictum urna lacinia. Praesent rhoncus mi tellus, pulvinar ultrices sapien dictum non. Suspendisse porta sit amet sem vel suscipit.
                                                   Vestibulum convallis lacus diam, sit amet fringilla odio congue et. Pellentesque molestie vulputate est, convallis viverra ante suscipit sed. Nam faucibus fringilla purus ut feugiat. Phasellus venenatis vulputate risus at maximus. Donec scelerisque eu tellus blandit lobortis. Nunc ornare ultrices 
                                                   leo vitae scelerisque. Vivamus hendrerit tellus vitae sapien aliquet pretium. Sed lacinia at nunc placerat lacinia. Suspendisse nunc est, ultrices vel pulvinar sit amet, molestie vel felis. Mauris consequat odio eu ante dictum tristique. Quisque bibendum ligula accumsan neque venenatis auctor. 
                                                   Nulla facilisi. Sed vel rutrum turpis, sed ultricies dui. Vivamus aliquam venenatis odio, vitae pharetra odio faucibus non.
                                                   Praesent tempus enim diam, ullamcorper eleifend justo facilisis ac. Vestibulum vestibulum facilisis risus, nec venenatis erat condimentum eu. Sed imperdiet, augue sit amet finibus molestie, neque nibh finibus tortor, vitae porta ante orci et enim. Aliquam quis nisl vel enim eleifend blandit. 
                                                   Pellentesque nec lectus neque. Duis interdum elit vitae orci maximus, ac pulvinar neque faucibus. Curabitur enim elit, consectetur in ipsum vel, ullamcorper aliquam augue. Mauris euismod mollis eros, id pellentesque neque pulvinar id. Sed nec fermentum lorem. In efficitur ante sit amet felis
                                                   lobortis hendrerit. Integer eget risus non magna maximus malesuada.
                                                   Nam hendrerit mi sit amet sapien faucibus tincidunt. Aliquam posuere auctor consectetur. Sed lacinia consectetur justo, vel tincidunt leo ullamcorper eu. Vestibulum consectetur ut sem eget varius. Donec id convallis elit. Maecenas dictum sit amet massa at facilisis. Quisque laoreet nunc vitae leo 
                                                   sagittis pellentesque. Sed lacinia enim tempus justo viverra molestie. Duis egestas interdum diam, sed egestas lectus facilisis sit amet. Proin et arcu ipsum. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc volutpat consectetur dui, sed finibus risus molestie eu. Phasellus sagittis eu 
                                                   erat rhoncus hendrerit. Quisque sit amet eros lacus. Praesent eget porta lorem.
                                                   Nulla eu pellentesque mauris. Nunc in porttitor elit, eget finibus nibh. Integer ac felis vel quam porta hendrerit vel pulvinar odio. Nulla rutrum molestie felis, at dapibus tellus varius eget. Praesent vel diam at nunc venenatis sagittis. Phasellus quis sagittis metus. Vivamus sed egestas nunc.";

    public static void Seed(this ApplicationDbContext data)
    {
      if (data.Projects.Any())
      {
        return;
      }

      var projects = CreateProject();
      data.Projects.AddRange(projects);

      var user = CreateUser();
      var userStore = new UserStore<User>(data);
      userStore.CreateAsync(user);

      var comment = CreateComment(data.Projects.FirstOrDefault().Id, data.Users.FirstOrDefault().Id);
      data.Comments.Add(comment);

      data.SaveChanges();
    }

    private static IEnumerable<Project> CreateProject()
    {
      var project = new Project();

      var projects = new List<Project>();
      for (int i = 1; i < 6; i++)
      {
        project = new Project
        {
          Title = $"Initial project{i}",
          ShortDescription = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
                             Curabitur eleifend, massa ut eleifend sodales, nisi lacus pellentesque lorem, 
                             sed sollicitudin felis diam ut est. Vivamus convallis pretium justo at rhoncus.",
          Description = description
        };

        projects.Add(project);
      }

      return projects;
    }

    private static User CreateUser()
    {
      var userName = "initialUser";
      var email = "initial@user.com";
      var password = "initialPassword";

      var user = new User
      {
        UserName = userName,
        NormalizedUserName = userName.ToUpper(),
        Email = email,
        NormalizedEmail = email.ToUpper()
      };

      var passwordHasher = new PasswordHasher<User>();
      var hashedPassword = passwordHasher.HashPassword(user, password);
      user.PasswordHash = hashedPassword;

      return user;
    }

    private static Comment CreateComment(int projectId, string userId)
    {
      var comment = new Comment
      {
        ProjectId = projectId,
        Content = "Intially created comment for the first project",
        CreatedBy = userId
      };

      return comment;
    }
  }
}
