import { Pipe } from '@angular/core';
@Pipe({
  name:"TengePipe"
})
 class TengePipe {
    transform(value: string, fallback: string): string {
        return value+"тг(KZT)"
  }
}