import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: "searchFilter"
})
export class SearchFilterPipe implements PipeTransform {
    transform(value: any[], searchText: string): any[] {
        if (!value || !searchText) {
            return value;
        }

        searchText = searchText.toLowerCase();

        return value.filter((item: any) => {
            return item.name.toLowerCase().includes(searchText);
        });
    }
}