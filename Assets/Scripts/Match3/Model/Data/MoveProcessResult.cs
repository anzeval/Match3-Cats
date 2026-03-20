using System.Collections.Generic;

namespace Match3.Model.Data
{
    public class MoveProcessResult
    {
        public List<StepResult> CascadeSteps {get; private set;}
        public MoveResult SwapResult {get; private set;}
        public bool IsSuccess {get; private set;}

        // if our actions trigger a cascade
        public MoveProcessResult(List<StepResult> stepResults, MoveResult swapResult, bool isSuccess)
        {
            CascadeSteps = stepResults;
            SwapResult = swapResult;
            IsSuccess = isSuccess;
        }

        // If we cannot perform a swap because it is not nearby ot it is outside the field
        public MoveProcessResult(bool isSuccess)
        {
            CascadeSteps = new List<StepResult>();
            SwapResult = null;
            IsSuccess = isSuccess;
        }
    }   
} 
