import { HttpHeaders } from "@angular/common/http";

export abstract class BaseService {
  protected getHeaderJson() {
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
  }
}
