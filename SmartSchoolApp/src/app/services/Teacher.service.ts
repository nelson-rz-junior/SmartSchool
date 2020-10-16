import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Teacher } from '../models/Teacher';

@Injectable({
  providedIn: 'root'
})
export class TeacherService
{
  baseUrlV1 = `${environment.mainUrlApiV1}/professor`;
  baseUrlV2 = `${environment.mainUrlApiV2}/professor`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Teacher[]> {
    return this.http.get<Teacher[]>(this.baseUrlV1);
  }

  getById(id: number): Observable<Teacher> {
    return this.http.get<Teacher>(`${this.baseUrlV1}/${id}`);
  }

  getByDisciplineId(id: number): Observable<Teacher[]> {
    return this.http.get<Teacher[]>(`${this.baseUrlV1}/disciplina/${id}`);
  }

  getByStudentId(id: number): Observable<Teacher[]> {
    return this.http.get<Teacher[]>(`${this.baseUrlV2}/aluno/${id}`);
  }

  post(teacher: Teacher) {
    return this.http.post(this.baseUrlV1, teacher);
  }

  put(teacher: Teacher) {
    return this.http.put(`${this.baseUrlV1}/${teacher.id}`, teacher);
  }

  delete(id: number) {
    return this.http.delete(`${this.baseUrlV1}/${id}`);
  }
}
