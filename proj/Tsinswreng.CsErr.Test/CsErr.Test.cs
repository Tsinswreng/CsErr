using Tsinswreng.CsErr;
using Microsoft.Extensions.DependencyInjection;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test;

/// 測試執行入口。
/// 順序不能顛倒: 先備好依賴來源、再 Init、最後交給執行器。
internal class Program{
	public static IServiceCollection SvcColct = new ServiceCollection();
	public static IServiceProvider SvcProvdr = null!;

	public static async Task Main(string[] args){
		// 被測的都是純數據與純函數, 測試類不需要外部服務, 服務集合留空即可。
		var mgr = CsErrTestMgr.Inst;
		SvcProvdr = mgr.InitSvc(SvcColct, sc => sc.BuildServiceProvider());

		ITestExecutor executor = new TreeTestExecutor();
		await executor.RunEtPrint(mgr.TestNode);
	}
}