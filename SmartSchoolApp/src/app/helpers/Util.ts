export class Util {
  static concatArray(item: any[]): string {
    return item != null ? item.map(x => x.nome).join(', ') : '';
  }

  static getFirstName(fullName: string): string {
    const index = fullName.indexOf(' ');
    return fullName.substring(0, index);
  }

  static getLastName(fullName: string): string {
    const index = fullName.indexOf(' ');
    return fullName.substring(index + 1);
  }
}
