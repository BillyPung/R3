﻿
namespace R3.Tests.FactoryTests;

public class EveryValueChangedTest
{
    [Fact]
    public void EveryValueChanged()
    {
        var frameProvider = new FakeFrameProvider();

        var t = new Target();
        t.MyProperty = 99;

        var list = Observable.EveryValueChanged(t, x => x.MyProperty, frameProvider).ToLiveList();

        list.AssertEqual([99]);

        t.MyProperty = 100;
        frameProvider.Advance();

        list.AssertEqual([99, 100]);

        t.MyProperty = 100;
        frameProvider.Advance();

        list.AssertEqual([99, 100]);

        t.MyProperty = 1000;
        frameProvider.Advance();

        list.AssertEqual([99, 100, 1000]);

        frameProvider.GetRegisteredCount().ShouldBe(1);

        list.Dispose();
        frameProvider.Advance();

        frameProvider.GetRegisteredCount().ShouldBe(0);
    }
}

file class Target
{
    public int MyProperty { get; set; }
}
