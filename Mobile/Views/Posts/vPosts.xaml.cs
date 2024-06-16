using Mobile.Services;
using Mobile.ViewModels;
using Shareds.Modeles;

namespace Mobile.Views.Posts;

public partial class vPosts : ContentPage
{
    UserService userService;
    ParkService parkService;
    PostService postService;
    RoomService roomService;

    List<PosteModele> postsToDisplay;
    PostViewModel PostVM;

    public vPosts()
	{
		InitializeComponent();

        userService = new UserService();
        parkService = new ParkService();
        postService = new PostService();
        roomService = new RoomService();

        postsToDisplay = new();

        PostVM = (PostViewModel)BindingContext;
        PostVM.IsLabelVisible = true;
        PostVM.IsContentVisible = false;

        _ = GetAllPosts();
    }

    public async Task GetAllPosts()
    {
        try
        {
            var userSession = userService.GetUserPreferences();
            var parkSession = parkService.GetParkPreferences();

            if (userSession != null && parkSession != null)
            {
                await Task.Delay(1000);
                var posts = await postService.GetAllByParc(parkSession.ParcId, userSession.Token);
                var rooms = await roomService.GetAllByParc(parkSession.ParcId, userSession.Token);

                PostVM.Rooms = rooms;

                foreach (var room in rooms)
                {
                    foreach (var post in posts)
                    {
                        if (post.IdSalle == room.Id)
                        {
                            post.IdSalle = room.Numero;
                            postsToDisplay.Add(post);
                        }
                    }
                }

                if (postsToDisplay != null)
                {
                    PostVM.Posts = postsToDisplay;

                    PostVM.IsLabelVisible = false;
                    PostVM.IsContentVisible = true;
                }
                else
                {
                    lbl_loading.Text = "Merci de vous rendre sur notre plateforme web pour cr�er des postes.";
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        SalleModele room = ((Picker)sender).SelectedItem as SalleModele;

        if (room != null)
        {
            PostVM.Posts = postsToDisplay.Where(p => p.IdSalle == room.Numero).ToList();
        }
        else
        {
            PostVM.Posts = postsToDisplay;
        }
    }
}