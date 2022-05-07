using CategoryManager.Model;

namespace CategoryManager.Utils;

internal static class Constants
{
  public static Observation[] observations = new Observation[]
  {
    //related
    //1
    new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,0,0,0 } },
    
    //3
    new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,0,1,0 } },
    
    //4
    new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,0,1,1 } },
    new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,0,1,1 } },
    
    //6
    new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,1,0,1 } },
    new Observation{ CategoryId = 1, IsRelated = true, ObservedObject = new int[] {0,1,0,1 } },
    
    //not related
    //6
    new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {0,1,0,1 } },
    
    //10
    new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,0,0,1 } },
    new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,0,0,1 } },
    
    //13
    new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,1,0,0 } },
    
    //14
    new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,1,0,1 } },
    new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,1,0,1 } },
    new Observation{ CategoryId = 1, IsRelated = false, ObservedObject = new int[] {1,1,0,1 } },
  };
}
