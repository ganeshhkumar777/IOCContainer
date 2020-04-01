using System;
using System.Reflection;
using Xunit;
using IOCContainer;
namespace IOCContainer.UnitTest
{
    public class AddDataToIOCContainerTest
    {

        [Fact]
        public void RegisterClassWithNoDependencyAndCheckWhetherInstanceIsCreated(){
            IOCContainerBuilder builder=new IOCContainerBuilder();
            builder.Add<ParameterlessConstructorTest,ParameterlessConstructorTest>();

            var result = builder.Get(typeof(ParameterlessConstructorTest));

            var obj= Activator.CreateInstance(result);

            Assert.IsType<ParameterlessConstructorTest>(obj);
            Assert.IsType<ParameterlessConstructorTest>(obj);
        }
         [Fact]
        public void RegisterClassWithSingleDependencyAndCheckWhetherInstanceIsCreated()
        {
            IOCContainerBuilder builder=new IOCContainerBuilder();
            builder.Add<iclassA,classA>();
            builder.Add<iclassB,classB>();
            
        
            var result = builder.Get(typeof(iclassA));
            ConstructorInfo[] info =result.GetConstructors(BindingFlags.Public|BindingFlags.Instance);

            ParameterInfo[] parameterInfos=info[0].GetParameters();
            object[] parameters=new object[parameterInfos.Length];
            for (int i=0; i < parameterInfos.Length;i ++)
            {
                Type typeofparams=parameterInfos[i].ParameterType;
                parameters[i]=Activator.CreateInstance(builder.Get(typeofparams));
            }
            var obj= Activator.CreateInstance(result,parameters);
            //Console.WriteLine(
                (obj as classA).print();
                //);
        }
        
        [Fact]
        public void RegisterClassWithSingleDependencyAndCheckWhetherYouCanAccessFunctionWithinThat()
        {
            IOCContainerBuilder builder=new IOCContainerBuilder();
            builder.Add<iclassA,classA>();
            builder.Add<iclassB,classB>();
            
        
            var result = builder.Get(typeof(iclassA));
            ConstructorInfo[] info =result.GetConstructors(BindingFlags.Public|BindingFlags.Instance);

            ParameterInfo[] parameterInfos=info[0].GetParameters();
            object[] parameters=new object[parameterInfos.Length];
            for (int i=0; i < parameterInfos.Length;i ++)
            {
                Type typeofparams=parameterInfos[i].ParameterType;
                parameters[i]=Activator.CreateInstance(builder.Get(typeofparams));
            }
            var obj= Activator.CreateInstance(result,parameters);
            
            Assert.True((obj as classA).print());
            
        }

        [Fact]
        public void RegisterClassWithSingleDependencyAndCheckWhetherYouCanAccessFunctionWithinThatOfItsChild()
        {
            IOCContainerBuilder builder=new IOCContainerBuilder();
            builder.Add<iclassA,classA>();
            builder.Add<iclassB,classB>();
            
        
            var result = builder.Get(typeof(iclassA));
            ConstructorInfo[] info =result.GetConstructors(BindingFlags.Public|BindingFlags.Instance);

            ParameterInfo[] parameterInfos=info[0].GetParameters();
            object[] parameters=new object[parameterInfos.Length];
            for (int i=0; i < parameterInfos.Length;i ++)
            {
                Type typeofparams=parameterInfos[i].ParameterType;
                parameters[i]=Activator.CreateInstance(builder.Get(typeofparams));
            }
            var obj= Activator.CreateInstance(result,parameters);
            
            Assert.True((obj as classA).classB.print());
            
        }
    }

    public class ParameterlessConstructorTest{
        public ParameterlessConstructorTest(){

        }
    }
    public interface iclassA{
    bool print();
    }
    public class classA: iclassA{
        public iclassB classB;
        public classA(iclassB classB){
            this.classB=classB;
        }
        public bool print(){
            Console.WriteLine(nameof(classA));
            return true;
        }
    }

    public interface iclassB{
        bool print();
    }
    public class classB: iclassB{
        public bool print(){
            Console.WriteLine(nameof(classA));
            return true;
        }
    }
}
