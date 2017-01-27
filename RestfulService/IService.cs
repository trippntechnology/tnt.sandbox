using System.ServiceModel;
using System.ServiceModel.Web;

namespace RestfulService
{
	[ServiceContract(Name="Service")]
	public interface IService
	{
		[OperationContract]
		[WebInvoke(ResponseFormat = WebMessageFormat.Json)]
		string SimpleMethod(string parameter);

		[OperationContract]
		[WebInvoke(ResponseFormat = WebMessageFormat.Json)]
		string ComplexMethod(ComplexParameter parameter);

		[OperationContract]
		[WebInvoke(ResponseFormat = WebMessageFormat.Json)]
		ReturnParameter ComplexReturn(ComplexParameter parameter);
	}
}
