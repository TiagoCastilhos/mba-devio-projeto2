import { environment } from "../../environments/environment";

export abstract class BaseService {
    protected apiUrl = environment.apiUrl;
}
