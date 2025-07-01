export type CustomResponse<T> = {
  data: T;
  success: boolean;
  errors: string[];
}
