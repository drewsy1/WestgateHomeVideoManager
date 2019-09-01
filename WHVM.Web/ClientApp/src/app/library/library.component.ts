import { Component, OnInit, OnDestroy } from '@angular/core';
import { PageDataService } from '../services/page-data.service';
import { FormControl } from '@angular/forms';
import { Source } from '../../interfaces/Source';
import { ApiManagerService } from '../services/api-manager.service';

@Component({
    selector: 'app-library',
    templateUrl: './library.component.html',
    styleUrls: ['./library.component.scss']
})
export class LibraryComponent implements OnInit, OnDestroy {
    sourceSearch = new FormControl('');
    sources: Source[];
    sourcesFiltered: Source[] = this.sources;

    sourceSearchChanges = this.sourceSearch.valueChanges.subscribe(
        (newData: string) =>
            (this.sourcesFiltered =
                newData === '' || newData === null
                    ? this.sources
                    : this.sources.filter(
                    (hero: Source) =>
                        !!hero.sourceName
                            .toLowerCase()
                            .includes(newData.toLowerCase())
                    ))
    );

    constructor(
        private pageDataService: PageDataService,
        private apiManagerService: ApiManagerService
    ) {
    }

    fetchData() {
        this.apiManagerService.getSources().subscribe((data: Source[]) => {
            this.sources = data;
            this.sourceSearch.reset();
        });
    }

    ngOnInit() {
        this.pageDataService.changePageTitle('Library');
        this.fetchData();
    }

    ngOnDestroy(): void {
        this.pageDataService.changePageTitle(undefined);
    }
}
