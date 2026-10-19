using System.Collections.Generic;
using System.Windows.Input;
using WalletWasabi.Fluent.ViewModels.Navigation;
using WalletWasabi.Helpers;

namespace WalletWasabi.Fluent.ViewModels.HelpAndSupport;

[NavigationMetaData(
	Title = "About Kovara",
	Caption = "Kovara Wallet version and project information",
	IconName = "info_regular",
	Order = 4,
	Category = "Help & Support",
	Keywords = new[]
	{
			"About", "Software", "Version", "Source", "Code", "Github", "Website", "Coordinator", "Status", "Stats", "Tor", "Onion",
			"User", "Support", "Bug", "Report", "FAQ", "Questions,", "Docs", "Documentation", "License", "Advanced", "Information",
			"Hardware", "Wallet"
	},
	NavBarPosition = NavBarPosition.None,
	NavigationTarget = NavigationTarget.DialogScreen)]
public partial class AboutViewModel : RoutableViewModel
{
	public AboutViewModel(UiContext uiContext, bool navigateBack = false) : base(uiContext)
	{
		EnableBack = navigateBack;

		Links = new List<ViewModelBase>()
			{
				new LinkViewModel(UiContext)
				{
					Link = DocsLink,
					Description = "Original project documentation",
					IsClickable = true
				},
				new SeparatorViewModel(UiContext),
				new LinkViewModel(UiContext)
				{
					Link = SourceCodeLink,
					Description = "Kovara source code (GitHub)",
					IsClickable = true
				},
				new SeparatorViewModel(UiContext),
				new LinkViewModel(UiContext)
				{
					Link = ClearnetLink,
					Description = "Original project website (Clearnet)",
					IsClickable = true
				},
				new SeparatorViewModel(UiContext),
				new LinkViewModel(UiContext)
				{
					Link = TorLink,
					Description = "Original project website (Tor)",
					IsClickable = false
				},
				new SeparatorViewModel(UiContext),
				new LinkViewModel(UiContext)
				{
					Link = UserSupportLink,
					Description = "Original project community support",
					IsClickable = true
				},
				new SeparatorViewModel(UiContext),
				new LinkViewModel(UiContext)
				{
					Link = BugReportLink,
					Description = "Report an upstream bug",
					IsClickable = true
				},
				new SeparatorViewModel(UiContext),
				new LinkViewModel(UiContext)
				{
					Link = FAQLink,
					Description = "Original project FAQ",
					IsClickable = true
				},
			};

		License = new LinkViewModel(UiContext)
		{
			Link = LicenseLink,
			Description = "MIT License",
			IsClickable = true
		};

		OpenBrowserCommand = ReactiveCommand.CreateFromTask<string>(x => UiContext.OpenBrowserAsync(x));

		ReleaseHighlightsDialogCommand = ReactiveCommand.CreateFromTask(async () => await Navigate().To().ReleaseHighlightsDialog().GetResultAsync());

		CopyLinkCommand = ReactiveCommand.CreateFromTask<string>(async (link) => await UiContext.Clipboard.SetTextAsync(link));

		NextCommand = CancelCommand;

		SetupCancel(enableCancel: false, enableCancelOnEscape: true, enableCancelOnPressed: true);
	}

	public List<ViewModelBase> Links { get; }

	public LinkViewModel License { get; }

	public ICommand ReleaseHighlightsDialogCommand { get; }

	public ICommand OpenBrowserCommand { get; }

	public ICommand CopyLinkCommand { get; }

	public Version ClientVersion => Constants.ClientVersion;

	public static string ClearnetLink => "https://wasabiwallet.io/";

	public static string TorLink => "http://wasabiukrxmkdgve5kynjztuovbg43uxcbcxn6y2okcrsg7gb6jdmbad.onion";

	public static string SourceCodeLink => "https://github.com/Nobioo7/WalletWasabi/";

	public static string UserSupportLink => "https://github.com/WalletWasabi/WalletWasabi/discussions/5185";

	public static string BugReportLink => "https://github.com/WalletWasabi/WalletWasabi/issues/new?template=bug-report.md";

	public static string FAQLink => "https://docs.wasabiwallet.io/FAQ/";

	public static string DocsLink => "https://docs.wasabiwallet.io/";

	public static string LicenseLink => "https://github.com/WalletWasabi/WalletWasabi/blob/master/LICENSE.md";
}
