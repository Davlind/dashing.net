namespace dashing.net.App_Start
{
	using System.Web.Optimization;
	using dashing.net.Infrastructure;

	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			var application = new ScriptBundle("~/bundles/application-js")
				.Include("~/Scripts/jquery-{version}.js")
				.Include("~/Scripts/jquery.signalR-{version}.js")
				.Include("~/Scripts/signalr-hubs.js")
				.Include("~/Scripts/es5-shim.js")
				.Include("~/Scripts/batman.js")
				.Include("~/Scripts/batman.jquery.js")
				.Include("~/Scripts/dashing.coffee")
				.Include("~/Scripts/jquery.leanModal.min.js")
				.Include("~/Scripts/jquery.knob.js")
				.Include("~/Scripts/d3.v3.js")
				.Include("~/Scripts/rickshaw.js")
				.Include("~/Scripts/jquery.gridster.js")
				.Include("~/Scripts/dashing.gridster.coffee")
				.IncludeDirectory("~/Widgets", "*.coffee", true)
				.Include("~/Scripts/application.coffee")
				.Include("~/Scripts/highcharts.js")
				.Include("~/Scripts/json2.js");

			application.EnableFileExtensionReplacements = false;
			application.Transforms.Add(new CoffeeTransform());
			bundles.Add(application);

			var styles = new Bundle("~/bundles/application-css")
				.Include("~/Content/css/font-awesome.css")
				.Include("~/Content/jquery.gridster.css")
				.IncludeDirectory("~/Widgets", "*.scss", true)
				.Include("~/Content/application.scss");

			styles.Transforms.Add(new ScssTransform());
			bundles.Add(styles);

			BundleTable.EnableOptimizations = true;
		}
	}
}