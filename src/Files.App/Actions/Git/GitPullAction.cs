﻿// Copyright (c) Files Community
// Licensed under the MIT License.

namespace Files.App.Actions
{
	internal sealed partial class GitPullAction : ObservableObject, IAction
	{
		private readonly IContentPageContext _context;

		public string Label
			=> Strings.GitPull.GetLocalizedResource();

		public string Description
			=> Strings.GitPullDescription.GetLocalizedResource();

		public RichGlyph Glyph
			=> new("\uE74B");

		public bool IsExecutable
			=> _context.CanExecuteGitAction;

		public GitPullAction()
		{
			_context = Ioc.Default.GetRequiredService<IContentPageContext>();

			_context.PropertyChanged += Context_PropertyChanged;
		}

		public Task ExecuteAsync(object? parameter = null)
		{
			return GitHelpers.PullOriginAsync(_context.ShellPage!.InstanceViewModel.GitRepositoryPath);
		}

		private void Context_PropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName is nameof(IContentPageContext.CanExecuteGitAction))
				OnPropertyChanged(nameof(IsExecutable));
		}
	}
}
