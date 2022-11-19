using FluentTube.App.Models;
using FluentTube.App.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentTube.App.ViewModels.Videos
{
    public class CommentsViewModel : ObservableObject
    {
        public CommentsViewModel(YouTubeService services)
        {
            _service = services;

            _commentThreads = new();
            CommentThreads = new(_commentThreads);

            LoadCommentsPageCommand = new AsyncRelayCommand(LoadCommentsPageAsync);
        }

        private readonly YouTubeService _service;

        private string _videoId;
        public string VideoId { get => _videoId; set => SetProperty(ref _videoId, value); }

        private readonly ObservableCollection<CommentThread> _commentThreads;
        public ReadOnlyObservableCollection<CommentThread> CommentThreads { get; }

        public IAsyncRelayCommand LoadCommentsPageCommand { get; }

        private async Task LoadCommentsPageAsync()
        {
            // Gets video top-level comment threads
            var requestCommentThread = _service.CommentThreads.List("snippet");
            requestCommentThread.VideoId = VideoId;
            requestCommentThread.Order = CommentThreadsResource.ListRequest.OrderEnum.Relevance;
            requestCommentThread.TextFormat = CommentThreadsResource.ListRequest.TextFormatEnum.PlainText;
            var responseCommentThread = await requestCommentThread.ExecuteAsync();

            foreach (var item in responseCommentThread.Items)
            {
                _commentThreads.Add(item);
            }
        }
    }
}
